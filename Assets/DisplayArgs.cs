using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayArgs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string[] args = System.Environment.GetCommandLineArgs ();
		Text text = GetComponent<Text>();
		foreach (string s in args){
			text.text += s + '\n';
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
