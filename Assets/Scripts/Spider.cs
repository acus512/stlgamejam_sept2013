using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {
	//public vars
	public float MovementRadius = 10f;
	public float MovementSpeed = 3f;
	public float AttackVisionRange = 3f;
	public float AttackRange = 1f;
	
	//private vars
	public Vector3 startPos;
	public Vector3 newPos;
	public float pointRange = .5f;
	public Vector3 flatTransform;
	public Vector3 flatNewPos;
	public float distanceToNewPos;
	public float distanceToPlayer;
	public GameObject player;
	public bool Attacking = false;

	// Use this for initialization
	void Start () {
		
		//set defaults
		startPos = transform.position;
		newPos = startPos;
		player = GameObject.Find("Player");
	}	
	
	// Update is called once per frame
	void Update () {
		
		flatTransform = new Vector3(transform.position.x, 0, transform.position.z);
		flatNewPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
		distanceToPlayer = Vector3.Distance(flatTransform, flatNewPos);
		
		if (AttackVisionRange > distanceToPlayer)
		{
			Attacking = true;
			
			
			if (AttackRange > distanceToPlayer)
			{
				//Attack player
				Debug.Log("Attacking Player");
				
			}
			else
			{
				//Move towards the player
				transform.LookAt(player.transform);
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
				//rigidbody.AddForce (transform.forward *MovementSpeed );
				rigidbody.MovePosition(transform.position + (transform.forward * MovementSpeed * Time.deltaTime) );
			}			
		}
		else
		{
			Attacking = false;
			flatTransform = new Vector3(transform.position.x, 0, transform.position.z);
			flatNewPos = new Vector3(newPos.x, 0, newPos.z);
			distanceToNewPos = Vector3.Distance(flatTransform, flatNewPos);
			
			if (pointRange > distanceToNewPos)
			{
				//Find a new random position within the movement radius
				newPos = startPos + (Random.insideUnitSphere * MovementRadius);
				transform.LookAt(newPos);
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			}
			else
			{
				//not at new position, so move some towards it.
				//transform.LookAt(newPos);
				//transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
				transform.LookAt(newPos);
				
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
				//rigidbody.AddForce (transform.forward *MovementSpeed );
				rigidbody.MovePosition(transform.position + (transform.forward * MovementSpeed * Time.deltaTime) );
			}
		}
		
		
	}
}
