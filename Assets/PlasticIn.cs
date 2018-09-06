using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticIn : MonoBehaviour {

	public Rect SpawnZone;
	public GameObject[] spawnables;
	public float averageSpawnFrequency;
	float nextSpawn;
	public float impulse;
	public bool reparent;
	public Transform reparentTarget;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		nextSpawn -= Time.deltaTime;
		if (nextSpawn<=0){
			nextSpawn += Random.value * (2 / averageSpawnFrequency);
			Score.record.Down();
			GameObject plastic = GameObject.Instantiate(spawnables[Random.Range(0, spawnables.Length)]);
			if (reparent)
				plastic.transform.parent = (reparentTarget != null ? reparentTarget : this.transform);
			plastic.transform.position = (Vector3) Sample(Random.value, Random.value) + Vector3.forward * transform.position.z;
			Rigidbody2D rig = plastic.GetComponent<Rigidbody2D>();
			if (rig!=null)
				rig.AddForce(transform.right * impulse, ForceMode2D.Impulse);
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine(
			Sample(0, 0),
			Sample(1, 0)
		);
		Gizmos.DrawLine(
			Sample(1, 0),
			Sample(1, 1)
		);
		Gizmos.DrawLine(
			Sample(1, 1),
			Sample(0, 1)
		);
		Gizmos.DrawLine(
			Sample(0, 1),
			Sample(0, 0)
		);
		Gizmos.DrawLine(
			Sample(0.25f, 0.25f),
			Sample(0.25f, 0.75f)
		);
		Gizmos.DrawLine(
			Sample(0.25f, 0.25f),
			Sample(0.75f, 0.5f)
		);
		Gizmos.DrawLine(
			Sample(0.75f, 0.5f),
			Sample(0.25f, 0.75f)
		);
	}

	public Vector2 Sample(float u, float v){
		Vector2 ret = new Vector2(
			Mathf.LerpUnclamped(SpawnZone.xMin, SpawnZone.xMax, u),
			Mathf.LerpUnclamped(SpawnZone.yMin, SpawnZone.yMax, v)
		);
		ret = ret.Rotate(transform.rotation.eulerAngles.z*Mathf.Deg2Rad);
		ret += (Vector2) transform.position;
		return ret;
	}

}
