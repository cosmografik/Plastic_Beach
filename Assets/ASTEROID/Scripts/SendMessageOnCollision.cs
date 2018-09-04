using UnityEngine;
using System.Collections;

public class SendMessageOnCollision : MonoBehaviour {

	public string message;
	public GameObject target;

	void OnCollisionEnter()
	{
		target.SendMessage (message);
	}
}
