using UnityEngine;
using System.Collections;

public class SendMessageOnCollisionToObjectsWithTag : MonoBehaviour
{
	public string tag;
	public string message;
	
	void OnCollisionEnter()
	{
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag(tag))
		{
			obj.SendMessage(message);
		}
	}
	
}
