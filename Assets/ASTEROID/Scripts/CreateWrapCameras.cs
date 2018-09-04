using UnityEngine;
using System.Collections;

public class CreateWrapCameras : MonoBehaviour {

	public GameObject wrapCam;

	private Vector3 horizontalOffset;
	private Vector3 verticalOffset;
	private Vector3 zOffset;

	private int oldScreenWidth;
	private int oldScreenHeight;

	// Use this for initialization
	void Start ()
	{
		LayoutCameras ();
	}

	void Update()
	{
		if (oldScreenWidth != Screen.width || oldScreenHeight != Screen.height)
		{
			LayoutCameras();
		}
	}

	void LayoutCameras()
	{
		//Save current res information
		oldScreenWidth = Screen.width;
		oldScreenHeight = Screen.height;

		//Figure out the screen bounds
		horizontalOffset = Camera.main.ViewportToWorldPoint (new Vector3 (1.5f, 0.5f, -Camera.main.transform.position.z));
		verticalOffset = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 1.5f, -Camera.main.transform.position.z));
		zOffset = new Vector3 (0, 0, Camera.main.transform.position.z);
		
		//Destroy any wrap cameras (if they exist)
		foreach(GameObject cam in GameObject.FindGameObjectsWithTag("WrapCamera"))
		{
			Destroy (cam);
		}
		
		//Instantiate our cameras at the proper position
		Instantiate (wrapCam, transform.position + horizontalOffset, wrapCam.transform.rotation);
		Instantiate (wrapCam, transform.position - horizontalOffset, wrapCam.transform.rotation);
		Instantiate (wrapCam, transform.position + verticalOffset, wrapCam.transform.rotation);
		Instantiate (wrapCam, transform.position - verticalOffset, wrapCam.transform.rotation);
		
		Instantiate (wrapCam, transform.position + horizontalOffset + verticalOffset, wrapCam.transform.rotation);
		Instantiate (wrapCam, transform.position + horizontalOffset - verticalOffset, wrapCam.transform.rotation);
		Instantiate (wrapCam, transform.position - horizontalOffset + verticalOffset, wrapCam.transform.rotation);
		Instantiate (wrapCam, transform.position - horizontalOffset - verticalOffset, wrapCam.transform.rotation);
	}
}