using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : MonoBehaviour {

	public KeyCode open = KeyCode.DownArrow;
	public float closedAngle = 0;
	public float openAngle = 30;
	public float smoothTime = 0.5f;
	float current;
	float vel;

	// Use this for initialization
	void Start () {
		current = closedAngle;
	}
	
	// Update is called once per frame
	void Update () {
		current = Mathf.SmoothDampAngle(current, (open.Held() ? openAngle : closedAngle), ref vel, smoothTime);
		transform.localRotation = Quaternion.AngleAxis(current, Vector3.forward);
	}
}
