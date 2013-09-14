using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
	public Color color1;
	public Color color2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		light.color = new Color(Random.Range(color1.r, color2.r), Random.Range(color1.g, color2.g), Random.Range(color1.b, color2.b), 1);
	}
}
