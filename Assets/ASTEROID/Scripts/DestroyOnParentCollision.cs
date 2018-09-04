using UnityEngine;
using System.Collections;

public class DestroyOnParentCollision : MonoBehaviour {

	void OnCollisionEnter ()
	{
		Destroy (transform.parent.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
