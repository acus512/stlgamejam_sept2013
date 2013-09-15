using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {
	
	public bool won = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider c)
	{
		won = true;
		Debug.Log("Won!");
		Application.LoadLevel("Credits");
	}
	
}
