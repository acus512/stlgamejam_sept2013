using UnityEngine;
using System.Collections;

public class DamageDealerTestOnly : MonoBehaviour {
	public float AttackVisionRange = 3f;
	public float AttackRange = 1f;
	public float AttackDamage = 10f;
	public float TimeBetweenAttacks = .5f;
	public float DistanceToEnemy;
	private Spider[] Enemies;
	public bool Attacking = false;
	public float lastAttack = 0f;
	private Vector3 flatTransform;
	private Vector3 flatNewPos;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Enemies = GameObject.FindObjectsOfType(typeof(Spider)) as Spider[];
		
		foreach(Spider enemy in Enemies )
		{
			flatTransform = new Vector3(transform.position.x, 0, transform.position.z);
			flatNewPos = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
			DistanceToEnemy = Vector3.Distance(flatTransform, flatNewPos);
			
			if (AttackVisionRange > DistanceToEnemy)
			{
				Attacking = true;
				
				
				if (AttackRange > DistanceToEnemy)
				{
					//Attack player
					if (lastAttack > TimeBetweenAttacks)
					{
						enemy.SendMessage("TakeDamage",AttackDamage,SendMessageOptions.DontRequireReceiver);
						lastAttack = 0f;
					}
					else
					{
						lastAttack += Time.deltaTime;	
					}
				}
				else
				{
					//Move towards the player
					transform.LookAt(enemy.transform);
					transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
					//rigidbody.AddForce (transform.forward *MovementSpeed );
					//rigidbody.MovePosition(transform.position + (transform.forward * MovementSpeed * Time.deltaTime) );
				}			
			}			
		}
		
		
	}
}
