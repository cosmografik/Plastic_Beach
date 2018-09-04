using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMove : MonoBehaviour {

	Rigidbody2D rig;
	public KeyCode up = KeyCode.UpArrow;
	public KeyCode down = KeyCode.DownArrow;
	public KeyCode left = KeyCode.LeftArrow;
	public KeyCode right = KeyCode.RightArrow;

	public float smooth;
	Vector2 smVel;
	Vector2 moveVel;
	public float speed;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 move = Vector2.zero;
		if (up.Held()){
			move+=Vector2.up * Time.deltaTime * speed;
		}
		if (down.Held()){
			move+=Vector2.down * Time.deltaTime * speed;
		}
		if (left.Held()){
			move+=Vector2.left * Time.deltaTime * speed;
		}
		if (right.Held()){
			move+=Vector2.right * Time.deltaTime * speed;
		}
		moveVel = Vector2.SmoothDamp(moveVel, move, ref smVel, smooth);
		rig.MovePosition(rig.position + moveVel);
	}
}


public static class KeyCodePressedExtention {
	public static bool Held(this KeyCode keyCode){
		return Input.GetKey(keyCode);
	}
	public static bool Down(this KeyCode keyCode){
		return Input.GetKeyDown(keyCode);
	}
	public static bool Up(this KeyCode keyCode){
		return Input.GetKeyUp(keyCode);
	}
}