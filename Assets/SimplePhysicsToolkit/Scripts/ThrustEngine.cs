﻿using UnityEngine;
using System.Collections;

public class ThrustEngine : MonoBehaviour {

	public bool thrustEnabled = true; //System is on and thrusters will fire
	public float maxPower = 500.0f;
	public float currentPowerPercentage = 0.25f;

	public bool hoverMode = false;
	public float hoverDistance = 2.0f;
	public float hoverSafeRange = 0.5f;

	public Rigidbody boundObject;

	Rigidbody thruster;
	bool forceDisableThruster = false;

	float lastHoverPerc = 0.0f;

	void Start () {
		if (boundObject != null) {
			//Targeting a specific gameObejct
			thruster = boundObject;
		} else if (GetComponent<Rigidbody> () != null) {
			thruster = GetComponent<Rigidbody> ();
		} else {
			forceDisableThruster = true;
		}

		if (forceDisableThruster != true) {
			if (hoverMode) {
				float theMass = thruster.mass;
				thruster.drag = theMass / 10;
				thruster.angularDrag = theMass / 10;
			}
		}
	}
	

	void Update () {
		if (thrustEnabled && forceDisableThruster != true) {
			if (hoverMode) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position, -Vector3.up, out hit, hoverDistance + hoverSafeRange)) {

					if (hit.distance < hoverDistance) {
						if (thruster.velocity.magnitude > 0.5) {
							lastHoverPerc = thruster.velocity.magnitude / 50;
							applyThrustHover (lastHoverPerc);
						} else {
							applyThrustHover (lastHoverPerc);
						}
					}
				}

			} else {
				applyThrust ();
			}
		}
	}

	void applyThrust(){
		thruster.AddForceAtPosition (transform.up * (maxPower * currentPowerPercentage), transform.position, ForceMode.Force);
	}

	void applyThrustHover(float force){
		thruster.AddForceAtPosition (Vector3.up * (maxPower * force), transform.position, ForceMode.Acceleration);
	}

	void OnDrawGizmos(){
		if (hoverMode) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine (transform.position, transform.position - transform.up * hoverDistance);

			Gizmos.color = Color.cyan;
			Gizmos.DrawLine (transform.position, transform.position + transform.up * hoverSafeRange);
		} else {
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine (transform.position, transform.position - transform.up * 2.0f);
		}
	}
}
