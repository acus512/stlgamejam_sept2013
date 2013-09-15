using UnityEngine;
using System.Collections;

public class ThumperTrigger : MonoBehaviour {
	
	GameObject thumper;

	
	// Use this for initialization
	void Start () {
		thumper = transform.FindChild ("thumper_mesh").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name != "Thumper" && thumper.animation.isPlaying == false)
		{
			thumper.animation.Play ("Take 001");
		}
    }
}
