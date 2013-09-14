using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	Transform _transform;
	Rigidbody _rigidbody;

	void Awake()
	{
		_transform = transform;
		_rigidbody = rigidbody;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Face in the direction of travel
		if (_rigidbody.velocity == Vector3.zero) {
			return;
		}

		_transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(_rigidbody.velocity, Vector3.up), 1);
	}
}
