using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject asteroid;
	public GameObject enemyShip;
	public GameObject playerShip;
	public float spawnRadius = 20.0f;
	public int level = 1;
	public int minimumAsteroids = 3;
	public float additionalAsteroidsPerLevel = 0.75f;
	public float timeBeforeEnemyShip = 60.0f;
	public float timeBeforeEnemyShipMultiplier = 0.75f;
	public int lives = 3;

	private int score = 0;
	private bool gameOver = false;

	// Use this for initialization
	void Start ()
	{
		SpawnAsteroids ();
		Invoke ("SpawnEnemyShip", timeBeforeEnemyShip);
	}

	void SpawnEnemyShip()
	{
		//Spawn our enemy ship
		Instantiate (enemyShip,(Vector3)Random.insideUnitCircle* spawnRadius, Quaternion.identity);
		Invoke ("SpawnEnemyShip", timeBeforeEnemyShip);
	}


	void SpawnAsteroids()
	{
		for (int i =0; i < Mathf.RoundToInt( level * additionalAsteroidsPerLevel) + minimumAsteroids; i++) 
		{
			//Get random position
			Vector3 spawnPosition = (Vector3)Random.insideUnitCircle* spawnRadius;

			int failsafe = 0;
			//Check to see if there's something where we wish to spawn
			while(Physics.CheckSphere(spawnPosition, asteroid.transform.localScale.x) && failsafe < 100)
			{
				spawnPosition = (Vector3)Random.insideUnitCircle* spawnRadius;
				failsafe++; 
			}

			//Spawn our asteroids
			Instantiate (asteroid,spawnPosition, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!GameObject.FindGameObjectWithTag("Asteroid"))
		{
			NextLevel();
		}
		if (gameOver && Input.GetKeyDown(KeyCode.Return))
		{
			ResetGame();
		}
	}

	void LoseLife()
	{
		if (lives > 0) 
		{
			lives--;
			StartCoroutine (SpawnPlayer());
		}
		else
		{
			gameOver = true;
		}
	}

	IEnumerator SpawnPlayer()
	{
		while(Physics.CheckSphere(Vector3.zero,1.5f))
		{
			yield return new WaitForSeconds(0.5f);
		}
		Instantiate (playerShip, Vector3.zero, playerShip.transform.rotation);
	}

	void NextLevel()
	{
		GetComponent<AudioSource>().Play ();
		level++;
		SpawnAsteroids();
		CancelInvoke ();
		Invoke ("SpawnEnemyShip", timeBeforeEnemyShip*(Mathf.Pow(timeBeforeEnemyShipMultiplier, level)));
	}

	void ResetGame()
	{
		foreach(GameObject oldAsteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
		{
			Destroy(oldAsteroid);
		}
		StartCoroutine (SpawnPlayer ());
		gameOver = false;
		GetComponent<AudioSource>().Play ();
		level = 1;
		lives = 3;
		SpawnAsteroids();
		CancelInvoke ();
		Invoke ("SpawnEnemyShip", timeBeforeEnemyShip*(Mathf.Pow(timeBeforeEnemyShipMultiplier, level)));
	}

	void OnGUI()
	{
		GUILayout.Label ("Lives : " + lives.ToString ());
		GUILayout.Label ("Score : " + score.ToString ());
		if (gameOver)
		{
			GUILayout.Label ("Game Over, press Return to restart!");
		}
	}
}
