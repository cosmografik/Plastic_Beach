using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {

	public GameObject bullet;
	public float bulletForce = 1000.0f;
	public float fireDelay = 1.0f;
	public float bulletInstantiationRadius = 2.0f;
	public float accuracy = 1.0f;

	public float shipSpeed = 1.0f;
	public float minTimeBeforeDirectionChange = 1.0f;
	public float maxTimeBeforeDirectionChange = 5.0f;

	public Vector3 moveDirection;

	// Call this function every fireDelay per second
	void Start ()
	{
		InvokeRepeating ("Shoot", fireDelay, fireDelay);

		ChangeDirection ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += moveDirection * shipSpeed * Time.deltaTime;
	}

	void Shoot ()
	{
		//Get our player by its tag
		GameObject target = GameObject.FindWithTag ("Player");

		if (target == null) 
		{
			target = GameObject.FindWithTag("Asteroid");
		}

		if (target != null)
				{
						//Determine direction to shoot
						Vector3 shotDirection = (target.transform.position - transform.position).normalized;

						Vector3 modifiedShotDirection = (shotDirection + (Vector3)(Random.insideUnitCircle * (1.0f - accuracy))).normalized;

						//Instantiate the bullet
						GameObject bulletInstance = Instantiate (bullet, transform.position + modifiedShotDirection * bulletInstantiationRadius, Quaternion.identity) as GameObject;

						//Throw it at the player
						bulletInstance.GetComponent<Rigidbody>().AddForce (modifiedShotDirection * bulletForce);
				}
	}

	void ChangeDirection ()
	{
		moveDirection = Random.insideUnitCircle.normalized;
		Invoke ("ChangeDirection", Random.Range(minTimeBeforeDirectionChange,maxTimeBeforeDirectionChange));
	}
}
