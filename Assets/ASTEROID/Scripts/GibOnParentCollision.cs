using UnityEngine;
using System.Collections;

public class GibOnParentCollision : MonoBehaviour {

	public GameObject [] gibs;
	public int [] amounts;

	public float spawnRadius = 0.5f;
	public float explosionForce = 500.0f;
	public float explosionRadius = 1.0f;

	void OnCollisionEnter ()
	{
		int gibIndex = 0;
		foreach(GameObject gib in gibs)
		{
			for (int amountIndex = 0; amountIndex < amounts[gibIndex]; amountIndex++)
			{
				GameObject gibInstance = Instantiate(gib, transform.position + Random.insideUnitSphere*spawnRadius,Quaternion.identity) as GameObject;
				if(gibInstance.GetComponent<Rigidbody>())
				{
					gibInstance.GetComponent<Rigidbody>().AddExplosionForce(explosionForce,transform.position,explosionRadius);
				}
			}
			gibIndex++;
		}

		Destroy (transform.parent.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
