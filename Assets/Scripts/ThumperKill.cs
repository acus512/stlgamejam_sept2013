using UnityEngine;
using System.Collections;

public class ThumperKill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		other.gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
    }
}
