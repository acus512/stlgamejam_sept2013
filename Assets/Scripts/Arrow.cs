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
		if (_rigidbody.velocity == Vector3.zero || _rigidbody.isKinematic) {
			return;
		}

		_transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(_rigidbody.velocity, Vector3.up), 1);
	}

	void OnCollisionEnter(Collision collision)
	{
		_rigidbody.isKinematic = true;

		if (collision.transform.tag == "Enemy") {
			//Deal damage to enemy
			transform.parent = collision.transform;
			collision.transform.SendMessage("TakeDamage", 100,SendMessageOptions.DontRequireReceiver);
		}
	}
}
