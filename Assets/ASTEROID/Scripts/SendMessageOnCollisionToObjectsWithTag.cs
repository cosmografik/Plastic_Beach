using UnityEngine;
using System.Collections;

public class SendMessageOnCollisionToObjectsWithTag : MonoBehaviour
{
	public string withThisTag;
	public string message;
	
	void OnCollisionEnter()
	{
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag(withThisTag))
		{
			obj.SendMessage(message);
		}
	}
	
}
