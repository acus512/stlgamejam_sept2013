using UnityEngine;
using System.Collections;

public class KingLaugh : MonoBehaviour {
	public AudioClip aClip;
	bool won = false;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(won && GetComponent<AudioSource>().isPlaying)
		{
			PlayerPrefs.SetString("won", "true");
		}
	}
	
	void OnTriggerEnter(Collider c)
	{
		
		Debug.Log ("Entered Trigger");
		GetComponent<AudioSource>().PlayOneShot(aClip);
		won = true;
	}
	
	void OnCollisionEnter(Collision c)
	{
		Debug.Log ("Entered Collision");
		GetComponent<AudioSource>().PlayOneShot(aClip);
		won = true;
	}
}
