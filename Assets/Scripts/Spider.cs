using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {
	enum Animations
	{
		Walk,
		Attack,
		Idle	
	}
	
	//public vars
	public float MovementRadius = 10f;
	public float MovementSpeed = 3f;
	public float AttackVisionRange = 3f;
	public float AttackRange = 1f;
	public float AttackDamage = 10f;
	public float TimeBetweenAttacks = .5f;
	
	//private vars
	Vector3 startPos;
	Vector3 newPos;
	float pointRange = .5f;
	Vector3 flatTransform;
	Vector3 flatNewPos;
	float distanceToNewPos;
	float distanceToPlayer;
	GameObject player;
	bool Attacking = false;
	float lastAttack = 0f;
	public bool die = false;
	float lastDeathFlash = 0;
	int deathFlashCount = 0;
	GameObject walk;
	GameObject attack;
	Animations CurAnimation;
	
	
	// Use this for initialization
	void Start () {
		
		//set defaults
		startPos = transform.position;
		newPos = startPos;
		player = GameObject.Find("Player");
		walk = transform.FindChild ("Walk").gameObject.transform.FindChild("spider_1").gameObject;
		attack = transform.FindChild("Attack").gameObject.transform.FindChild("spider_1").gameObject;
		CurAnimation = Animations.Walk;
		
	}	
	
	// Update is called once per frame
	void Update () {
		if (die == false)
		{
			flatTransform = new Vector3(transform.position.x, 0, transform.position.z);
			flatNewPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
			distanceToPlayer = Vector3.Distance(flatTransform, flatNewPos);
			
			if (AttackVisionRange > distanceToPlayer)
			{
				Attacking = true;
				
				
				if (AttackRange > distanceToPlayer)
				{
					//change to the attack mesh
					if (CurAnimation != Animations.Attack)
					{
						CurAnimation = Animations.Attack;
						walk.renderer.enabled = false;
						attack.renderer.enabled = true;
						//attack.animation.Play("Take 001");							
					}
					
					
					//Attack player
					if (lastAttack > TimeBetweenAttacks)
					{
						
						
						player.SendMessage("TakeDamage",AttackDamage,SendMessageOptions.DontRequireReceiver);
						lastAttack = 0f;
					}
					else
					{
						lastAttack += Time.deltaTime;	
					}
				}
				else
				{
					//change to the attack mesh
					if (CurAnimation != Animations.Walk)
					{
						CurAnimation = Animations.Walk;
						walk.renderer.enabled = true;
						attack.renderer.enabled = false;
						//attack.animation.Play("Take 001");							
					}
					
					//Move towards the player
					transform.LookAt(player.transform);
					transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
					//rigidbody.AddForce (transform.forward *MovementSpeed );
					rigidbody.MovePosition(transform.position + (transform.forward * MovementSpeed * Time.deltaTime) );
				}			
			}
			else
			{
				//change to the attack mesh
				if (CurAnimation != Animations.Walk)
				{
					CurAnimation = Animations.Walk;
					walk.renderer.enabled = true;
					attack.renderer.enabled = false;
					//attack.animation.Play("Take 001");							
				}
					
				
				lastAttack = 0f;
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
		else
		{
			//Spider has been killed.  Play death animation then destroy.
			if (deathFlashCount >= 10)
			{
				Destroy(gameObject);
			}
			else
			{
				if (lastDeathFlash > .2f)
				{
			
					switch(CurAnimation)
					{
						case Animations.Walk:
							walk.renderer.enabled = !walk.renderer.enabled;
							break;
						case Animations.Attack:
							attack.renderer.enabled = !attack.renderer.enabled;
							attack.transform.parent.GetComponent<Animator>().enabled = false;
							break;
					}
					
					/*
					Renderer[] rs = GetComponentsInChildren<Renderer>();

					foreach(Renderer r in rs)
					{
						r.enabled = !r.enabled ;
					}*/
     					
					
					
					
					deathFlashCount += 1;
					//transform.transform[0].renderer.enabled = !transform.renderer.enabled;
					lastDeathFlash = 0f;
				}
				else
				{
					lastDeathFlash += Time.deltaTime;					
				}
			}
		}
	}
	
	public void Die()
	{
		die = true;
	}
}
