﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameRunner : MonoBehaviour {

	bool killdest;
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
		if (!killdest && startTime+maxTime<Time.time){
			killdest = true;
			GameOver();
		}
	}

	public static void GameOver(){
		JsonPump.Dump<Record>(Score.record);
		if (Application.isEditor){
			//SceneManager.LoadScene("Scenes/GameOver");
		} else {
			Application.Quit();
		}
	}

}
