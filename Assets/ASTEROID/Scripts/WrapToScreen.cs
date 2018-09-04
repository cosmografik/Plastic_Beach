using UnityEngine;
using System.Collections;

public class WrapToScreen : MonoBehaviour
{
	private Vector3 horizontalOffset;
	private Vector3 verticalOffset;
	private float horizontalEdge;
	private float verticalEdge;

	private int oldScreenWidth;
	private int oldScreenHeight;


	// Use this for initialization
	void Start ()
	{
		GetScreenBounds ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (oldScreenWidth != Screen.width || oldScreenHeight != Screen.height)
		{
			GetScreenBounds();
		}
		//Check to see if the object has exceeded the screen bounds
		if (transform.position.x > horizontalEdge) 
		{
			transform.position -= horizontalOffset;
		}
		else if (transform.position.x < -horizontalEdge) 
		{
			transform.position += horizontalOffset;
		}
		if (transform.position.y > verticalEdge) 
		{
			transform.position -= verticalOffset;
		}
		else if (transform.position.y < -verticalEdge) 
		{
			transform.position += verticalOffset;
		}
	}

	void GetScreenBounds()
	{
		//Save current res information
		oldScreenWidth = Screen.width;
		oldScreenHeight = Screen.height;
		
		//Figure out the screen bounds
		horizontalOffset = Camera.main.ViewportToWorldPoint (new Vector3 (1.5f, 0.5f, -Camera.main.transform.position.z));
		verticalOffset = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 1.5f, -Camera.main.transform.position.z));
		horizontalEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1.0f, 0.5f, -Camera.main.transform.position.z)).x;
		verticalEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 1.0f, -Camera.main.transform.position.z)).y;
	}
}
