using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
	public Color color1;
	public Color color2;
	public Color newColor;
	
	private float duration = .05f;
	private float curTime = 0;
	// Use this for initialization
	void Start () {
		UpdateColor();
	}
	
	// Update is called once per frame
	void Update () {
		light.color = Color.Lerp(light.color, newColor,curTime/duration);
		curTime += Time.deltaTime;
		if(curTime >= duration)
		{
			curTime = 0;
			UpdateColor();
		}
	}
	
	void UpdateColor()
	{
		newColor = new Color(Random.Range(color1.r, color2.r), Random.Range(color1.g, color2.g), Random.Range(color1.b, color2.b), 1);
	}
}
