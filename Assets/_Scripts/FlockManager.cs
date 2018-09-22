using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

    public GameObject fishPrefab;
    public List<GameObject> fish;
    public int boundary;
    public int numFish;

	// Use this for initialization
	void Start ()
    {
        fish = new List<GameObject>();	
        
        for(int i = 0; i < numFish; i++)
        {
            Vector3 fishPosition = new Vector3(Random.Range(-boundary, boundary), Random.Range(-boundary, boundary), Random.Range(-boundary, boundary));
            GameObject newFish = Instantiate(fishPrefab, fishPosition, Quaternion.identity) as GameObject;
            newFish.GetComponent<FishController>().flock = this;
            fish.Add(newFish);
        }
	}
}
