using UnityEngine;
using System.Collections;

public class Thumper : MonoBehaviour {
	
	GameObject thumper;
	Collider player;
	
	// Use this for initialization
	void Start () {
		thumper = transform.FindChild ("thumper_mesh").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
    	if (thumper.animation.isPlaying == false)
		{
		
			player = other;
			thumper.animation.Play ("Take 001");
			
		}
		else
		{
			if (collider.name == "Thumper")
			{
				player.gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
			}
		}
				
			
		
    }
	
	
	
}
