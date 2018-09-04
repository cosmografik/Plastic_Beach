using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float lifeTime = 1.0f;

	// Use this for initialization
	void Start ()
	{
		Invoke ("SelfDestruct", lifeTime);
	}
	
	void SelfDestruct () 
	{
		Destroy (gameObject);
	}
}
