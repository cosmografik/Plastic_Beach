using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRoller : MonoBehaviour {

	Rigidbody2D rig;
	public KeyCode up = KeyCode.UpArrow;
	public KeyCode down = KeyCode.DownArrow;
	public KeyCode left = KeyCode.LeftArrow;
	public KeyCode right = KeyCode.RightArrow;

	public float sideSpeed;
	public float tugSpeed;


	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (up.Held()) {
			rig.rotation += tugSpeed * Time.deltaTime;
		}
		if (down.Held()) {
			rig.rotation -= tugSpeed * Time.deltaTime;
		}

	}
}
