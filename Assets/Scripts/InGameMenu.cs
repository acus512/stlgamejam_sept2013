using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {
	public Texture2D Checked;
	public Texture2D Unchecked;
	public Camera MainCamera;
	public OVRCameraController riftCamera;
	
	private bool invert = false;
	private bool oculus = false;
	
	void Start()
	{
		
	}
	
	void Update()
	{
		if(Input.GetMouseButton(0))
		{
			RaycastHit hit;
			
			if(oculus)
			{
				if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hit))
				{	
					if (hit.transform != null)
					{
						switch(hit.transform.name)
						{
							case "Invert Mouse":
								Debug.Log("Inverting Mouse");
								break;
							case "Enable Rift":
								riftCamera.enabled = false;
								MainCamera.enabled = true;
								Debug.Log("Toggling Rift");
								break;
						}
						
					}
				}
			}else{
				if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hit))
				{
					if (hit.transform != null)
					{
						switch(hit.transform.name)
						{
							case "Invert Mouse":
								Debug.Log("Inverting Mouse");
								break;
							case "Enable Rift":
								riftCamera.enabled = true;
								MainCamera.enabled = false;
								break;
						}
						
					}
				}	
			}
			
		}
	}
}
