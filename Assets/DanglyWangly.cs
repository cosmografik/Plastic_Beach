using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLink {
	public static float gravity;
	public static float pullback;
	public Vector2 velocity = Vector2.zero;
	public Vector2 position = Vector2.zero;
	public static float damping = 0.1f;
	public Vector2 target = Vector2.zero;
	public float length = 0.2f;
	public void Update(float dt){
		velocity -= velocity * damping * dt;
		velocity += Vector2.up * -gravity * dt;
		Vector2 nextPos = position + velocity * dt;
		Vector2 axle = nextPos - target;
		float sdist = axle.sqrMagnitude;
		if (sdist>=length*length){
			float angle = Mathf.Atan2(axle.y, axle.x);
			//*/
			velocity = Vector2.zero;
			/*/
			velocity = velocity.Rotate(-angle);
			velocity.x = 0;
			velocity = velocity.Rotate(angle);
			//*/
			velocity -= axle.normalized * pullback * dt;
			velocity += Vector2.up * -gravity * dt;
			nextPos = target + axle.normalized * length;
		}
		position = nextPos;
	}
}

public class DanglyWangly : MonoBehaviour {

	public float dangleGravity;
	public float danglePullback;
	public float dangleDamping;
	public KeyCode up = KeyCode.UpArrow;
	public KeyCode down = KeyCode.DownArrow;
	public float speed = 10;

	public float dtMultiplier;
	public int cycles;

	public float segmentLength;
	public float fullLength;
	
	public List<ChainLink> links;
	public Transform endPoint;

	public float minLength = 0.1f;
	public float maxLength = 20f;

	float chainLength {
		get {
			float l=0;
			for (int i = 0; i < links.Count; i++){
				l += links[i].length;
			}
			return l;
		}
	}


	// Use this for initialization
	void Start () {
		links = new List<ChainLink>();
		float length = fullLength;
		while (length>0){
			ChainLink l = new ChainLink();
			l.length = Mathf.Min(segmentLength, length);
			l.position = (Vector2) transform.position + Vector2.down * length;
			links.Insert(0,l);
			length -= segmentLength;
		}
	}
	
	// Update is called once per frame
	void Update () {
		ChainLink.gravity = dangleGravity;
		ChainLink.pullback = danglePullback;
		ChainLink.damping = dangleDamping;
		if (up.Held() != down.Held()){
			float scrollage = speed * Time.deltaTime;
			while (scrollage>0){
				if (up.Held()){
					if (chainLength-scrollage<=minLength) break;
					if (scrollage<links[0].length){
						links[0].length -= scrollage;
						scrollage = 0;
					} else {
						links.RemoveAt(0);
					}
				} else {
					if (chainLength+scrollage>=maxLength) break;
					if (links[0].length<segmentLength){
						float spaceleft = segmentLength - links[0].length;
						links[0].length += Mathf.Min(spaceleft, scrollage)+Mathf.Epsilon;
						scrollage -= spaceleft;
					} else {
						ChainLink l = new ChainLink();
						l.length = Mathf.Min(segmentLength, scrollage);
						l.position = transform.position;
						links.Insert(0, l);
						scrollage -= segmentLength;
					}
				}
			}
		}
		for (int _ = 0; _ < cycles; _++){
			for (int i = 0; i < links.Count; i++){
				links[i].target = (
					i == 0 ?
					new Vector2(transform.position.x,transform.position.y) :
					links[i - 1].position
				);
				links[i].Update((Time.deltaTime*dtMultiplier)/cycles);
			}
		}
		for (int i = 0; i < links.Count; i++) {
			Debug.DrawLine(
				(i == 0 ? transform.position : (Vector3)links[i - 1].position),
				links[i].position,
				(i % 2 == 0 ? Color.red : Color.green)
			);
		}
		Rigidbody2D rig = endPoint.GetComponent<Rigidbody2D>();
		if (rig)
			rig.MovePosition(links[links.Count - 1].position);
		else
			endPoint.position = links[links.Count - 1].position;
	}
}

 public static class Vector2Extension {
     
     public static Vector2 Rotate(this Vector2 v, float radian) {
         float sin = Mathf.Sin(radian);
         float cos = Mathf.Cos(radian);
         
         float tx = v.x;
         float ty = v.y;
         v.x = (cos * tx) - (sin * ty);
         v.y = (sin * tx) + (cos * ty);
         return v;
     }
 }
