using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	Transform _transform;
	Rigidbody _rigidbody;
	public AudioClip[] sounds;

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
		
		switch(collision.transform.tag)
		{
		case "Enemy":
			transform.parent = collision.transform;
			collision.transform.SendMessage("TakeDamage", 50,SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject.transform.FindChild("Capsule").collider);
			break;
		case "StartButton":
			Application.LoadLevel ("Spider Test Scene");
			break;
		case "ExitButton":
			Debug.Log("Chose to exit.");
			break;
		}
		
		GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(0,sounds.GetUpperBound(0))]);
	}
}
