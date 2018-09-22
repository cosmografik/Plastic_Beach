using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour {

    public float speedMin;
    public float speedMax;
    private float speed;
    public float rotationSpeed;
    public float neighbourDistance;
    public float avoidanceDistance;

    public FlockManager flock;

	// Use this for initialization
	void Start () {
        speed = Random.Range(speedMin, speedMax);
	}

    // Update is called once per frame
    void Update() {
        if (Vector3.Distance(Vector3.zero, transform.position) > flock.boundary)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            speed = Random.Range(speedMin, speedMax);
        }
        else
        {
            if (Random.Range(0, 100) < 20)
            {
                ApplyRules();
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
	}

    void ApplyRules()
    {
        List<GameObject> allFish = flock.fish;

        Vector3 vectorAvoid = Vector3.zero;
        Vector3 groupCentre = this.transform.position;
        Vector3 groupFacing = this.transform.forward;
        float groupSpeed = this.speed + 0.1f;

        int groupSize = 1;
        foreach(GameObject otherFish in allFish)
        {
            if(otherFish != this)
            {
                float seperation = Vector3.Distance(this.transform.position, otherFish.transform.position);
                //If the other fish is close enough to be in flock
                if(seperation <= neighbourDistance)
                {
                    groupCentre += otherFish.transform.position;
                    groupFacing += otherFish.transform.forward;
                    groupSize++;

                    //If other fish is too close then avoid
                    if(seperation <= avoidanceDistance)
                    {
                        vectorAvoid -= otherFish.transform.position - this.transform.position;
                    }
                    groupSpeed += otherFish.GetComponent<FishController>().speed;
                }
            }
        }

        //If this fish is in a flock apply flock rules
        if(groupSize > 1)
        {
            speed = groupSpeed / groupSize;
            groupFacing /= groupSize;

            groupCentre /= groupSize;
            Vector3 direction = groupCentre + vectorAvoid + groupFacing - this.transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
        }
    }
}
