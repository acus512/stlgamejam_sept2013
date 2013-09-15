using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {
	public Material Checked;
	public Material Unchecked;
	public GameObject nonRiftCamera;
	public GameObject riftCamera;
	public LayerMask HitMask;
	public GameObject invertCheckbox;
	public GameObject oculusCheckbox;
	public GameObject CursorObject;
	
	private bool invert = false;
	public bool oculus = false;
	
	void Start()
	{
		invert = bool.Parse(PlayerPrefs.GetString("Invert"));
	}
	void Update()
	{
		if(invert)
		{
			invertCheckbox.renderer.material = Checked;
		}else{
			invertCheckbox.renderer.material = Unchecked;
		}
		
		if(oculus)
		{
			oculusCheckbox.renderer.material = Checked;
		}else{
			oculusCheckbox.renderer.material = Unchecked;
		}
		
		RaycastHit hit;
		Transform leftRift = riftCamera.transform.FindChild("CameraRight");
		if(riftCamera.activeSelf)
		{
			if(Physics.Raycast(riftCamera.transform.position,leftRift.forward, out hit,100,HitMask))
			{
				Debug.DrawLine(riftCamera.transform.position,leftRift.position + (10*leftRift.forward),Color.yellow);
				if (hit.transform != null)
				{
					CursorObject.transform.position = hit.point;
				}
			}
		}
		
		if(Input.GetMouseButtonDown(0))
		{
			if(oculus)
			{
				if(Physics.Raycast(riftCamera.transform.position,leftRift.forward, out hit,100,HitMask))
				{
					if (hit.transform != null)
					{
						Debug.Log("Hit something: " + hit.collider.transform.name);
						switch(hit.collider.transform.name)
						{
							case "Invert Mouse":
								invert = !invert;
								PlayerPrefs.SetString("Invert",invert.ToString());
								Debug.Log("Inverting Mouse");
								break;
							case "Enable Rift":
								Debug.Log("Disable the rift");
								oculus = false;
								nonRiftCamera.camera.enabled = true;
								riftCamera.SetActive(false);
								break;
						}
					}
				}
			}else{
				if (Physics.Raycast(nonRiftCamera.camera.ScreenPointToRay(Input.mousePosition), out hit,100,HitMask))
				{
					if (hit.collider.transform != null)
					{
						Debug.Log("SHOULDN'T SEE ME ON THE RIFT");
						Debug.Log("Hit something: " + hit.collider.transform.name);
						switch(hit.collider.transform.name)
						{
							case "Invert Mouse":
								invert = !invert;
								PlayerPrefs.SetString("Invert",invert.ToString());
								Debug.Log("Inverting Mouse");
								break;
							case "Enable Rift":
								oculus = true;
								riftCamera.SetActive(true);
								nonRiftCamera.camera.enabled = false;
								break;
						}
						
					}
				}	
			}
			
		}
	}
}
