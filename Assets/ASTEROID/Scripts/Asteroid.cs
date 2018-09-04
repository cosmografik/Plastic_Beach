using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public int size = 2;
	public int pieces = 2;
	public float spacing = 1.0f;
	public GameObject mediumAsteroid;
	public GameObject smallAsteroid;
	public GameObject explosionSoundObject;

	// Use this for initialization
	void Start ()
	{
		if (size == 2)
		{
			GetComponent<Rigidbody>().AddForce (Random.insideUnitCircle * 100);
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (size == 2)
		{
			for (int i =0; i < pieces; i++)
			{
				GameObject instance = Instantiate(mediumAsteroid,transform.position + new Vector3(spacing * i,0,0), Quaternion.identity) as GameObject;
				instance.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + Random.insideUnitSphere * 10;
			}
			Instantiate (explosionSoundObject, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
		if (size == 1)
		{
			for (int i =0; i < pieces; i++)
			{
				GameObject instance = Instantiate(smallAsteroid,transform.position + new Vector3(spacing * i,0,0), Quaternion.identity) as GameObject;
				instance.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + Random.insideUnitSphere * 10;
			}
			Instantiate (explosionSoundObject, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
		else
		{
			Instantiate (explosionSoundObject, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
