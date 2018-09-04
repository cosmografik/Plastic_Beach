using UnityEngine;
using System.Collections;

public class DestroyWhenOffScreen : MonoBehaviour {

	void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}
}
