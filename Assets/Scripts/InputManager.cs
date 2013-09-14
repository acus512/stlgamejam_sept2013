using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	void Update()
	{
		if (Input.GetButtonDown("Fire1")) {
			ArrowShooter.Instance.FireArrow(Random.Range(0f, 1f));
		}
	}
}
