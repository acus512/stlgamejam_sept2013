using UnityEngine;
using System.Collections;

public class InstaKill : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider c)
	{
		Debug.Log("Collider Name: " + c.name);
		c.transform.BroadcastMessage("TakeDamage", 1000, SendMessageOptions.DontRequireReceiver);
	}
	
}