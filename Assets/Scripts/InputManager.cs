using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {
			ArrowShooter.Instance.FireArrow(Random.Range(0f, 1f));
		}
	}
}
