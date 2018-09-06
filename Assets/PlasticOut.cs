using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlasticEvent(GameObject plastic);

public class PlasticOut : MonoBehaviour {

	public string plasticTag = "plastic";
	public static PlasticEvent onPlasticOut;

	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag(plasticTag)){
			Score.record.Bump();
			if (onPlasticOut!=null){
				onPlasticOut(col.gameObject);
			}
			Destroy(col.gameObject);
		}
	}

}
