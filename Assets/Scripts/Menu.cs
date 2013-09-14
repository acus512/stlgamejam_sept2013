using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
		{
			RaycastHit hit;
			
			if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
			{
				if (hit.transform != null)
				{
					switch(hit.transform.name)
					{
						case "Start Game Button":
							Application.LoadLevel("Spider Test Scene");
							break;
						case "Exit Game Button":
							Debug.Log("Clicked exit!");
							break;
					}
					
				}
			}
		}
	}
}
