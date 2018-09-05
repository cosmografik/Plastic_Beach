using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameRunner : MonoBehaviour {

	public float maxTime = 30;
	float startTime;
	public float elapsed {
		get {
			return Time.time - startTime;
		}
	}
	public float remaining {
		get {
			return (startTime + maxTime) - Time.time;
		}
	}

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (startTime+maxTime<Time.time){
			GameOver();
		}
	}

	public static void GameOver(){
		if (Application.isEditor){
			//SceneManager.LoadScene("Scenes/GameOver");
		} else {
			Application.Quit();
		}
	}

}
