using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public float rotateSpeed = 60.0f;
	public float thrust = 100.0f;

	public GameObject bullet;
	public float bulletSpawnPosition = 1.0f;
	public float bulletSpeed = 1000.0f;

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.UpArrow))
		{
			GetComponent<Rigidbody>().AddForce(transform.forward*thrust*Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Rotate (0.0f,-rotateSpeed * Time.deltaTime,0.0f);
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Rotate (0.0f,rotateSpeed * Time.deltaTime,0.0f);
		}
		if (Input.GetKeyDown (KeyCode.Space))
		{
			GameObject instance = Instantiate(bullet, transform.position + transform.forward * bulletSpawnPosition, Quaternion.identity) as GameObject;
			instance.GetComponent<Rigidbody>().AddForce(transform.forward*bulletSpeed);
		}
	}
}
