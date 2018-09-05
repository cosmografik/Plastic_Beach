using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangleRenderer : MonoBehaviour {

	LineRenderer line;
	public DanglyWangly dangle;
	public float forwards;


	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		List<Vector3> points = new List<Vector3>();
		for (int i = 0; i < dangle.links.Count; i++) {
			points.Add((Vector3)dangle.links[i].position + Vector3.forward*forwards);
		}
		line.positionCount = dangle.links.Count;
		line.SetPositions(points.ToArray());
	}
}
