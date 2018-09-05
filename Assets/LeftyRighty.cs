using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftyRighty : MonoBehaviour {

	public KeyCode left = KeyCode.LeftArrow;
	public KeyCode right = KeyCode.RightArrow;

	public float speed;
	public float smooth;
	float current;
	float smVel;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float target = 0;
		if (left.Held())	target -= 1;
		if (right.Held())	target += 1;
		current = Mathf.SmoothDamp(
			current,
			target * speed,
			ref smVel,
			smooth
		);
		transform.position += Vector3.right * current * Time.deltaTime;
	}
}
