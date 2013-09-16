using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {
	
	public bool won = false;
	float wait = 35;
	float curTime = 0;
	public Texture2D texture;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		if(won)
		{
			curTime += Time.deltaTime;
			GUI.color = new Color (0,0,0,curTime/wait);
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),texture);
			if(curTime >= wait)
			{
				Application.LoadLevel("Credits");
			}
		}
	}
	
	void OnTriggerEnter(Collider c)
	{
		won = true;
	}
	
}
