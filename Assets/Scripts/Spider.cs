using UnityEngine;
using System.Collections;


public class Spider : MonoBehaviour {
	enum AnimationsEnum
	{
		Walk,
		Attack,
		Idle,
		Dead
	}
	
	//public vars
	public float MovementRadius = 10f;
	public float MovementSpeed = 3f;
	public float AttackVisionRange = 3f;
	public float AttackRange = 1f;
	public float AttackDamage = 10f;
	public float TimeBetweenAttacks = .5f;
	public AudioClip[] SoundList;
	public AudioClip AttackSound;
	public AudioClip SpiderHitSound;
	
	
	//private vars
	Vector3 startPos;
	Vector3 newPos;
	float pointRange = .5f;
	Vector3 flatTransform;
	Vector3 flatNewPos;
	public float distanceToNewPos;
	float distanceToPlayer;
	GameObject player;
	bool Attacking = false;
	float lastAttack = 0f;
	public bool die = false;
	float lastDeathFlash = 0;
	int deathFlashCount = 0;
	GameObject mesh;
	AnimationsEnum CurAnimation;
	float RotateSpeed = .01f;
	Quaternion newRot;
	float soundWait = 0f;
	float lastSound = 0f;
	AudioSource soundPlayer;
	
	// Use this for initialization
	void Start () {
		
		//set defaults
		startPos = transform.position;
		newPos = startPos;
		player = GameObject.Find("Player");
		mesh = transform.FindChild ("Walk").gameObject;
		CurAnimation = AnimationsEnum.Walk;
		soundWait = Random.Range(0f, 10f);
		soundPlayer = GetComponent<AudioSource>();
		

		
	}	
	
	// Update is called once per frame
	void Update () {
		if (die == false)
		{
			
			//Play sound if soundWait has elapsed, otherwise
			if (lastSound > soundWait && soundPlayer.isPlaying == false )
			{		
				int max = SoundList.Length;
				int i = Random.Range(0,max);
				soundPlayer.PlayOneShot(SoundList[i]);
				lastSound = 0f;
				soundWait = Random.Range(0f, 10f);

				
			}
			else
			{
				//continue waiting
				lastSound += Time.deltaTime;
			}
			
			
			
			if (player != null)
			{
				flatTransform = new Vector3(transform.position.x, 0, transform.position.z);
				flatNewPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
				distanceToPlayer = Vector3.Distance(flatTransform, flatNewPos);
			}
			else
			{
				distanceToPlayer = 100f;
			}
			
			if (AttackVisionRange > distanceToPlayer)
			{
				Attacking = true;
				
				
				if (AttackRange > distanceToPlayer)
				{
					//change to the attack mesh
					if (CurAnimation != AnimationsEnum.Attack)
					{
						CurAnimation = AnimationsEnum.Attack;
						mesh.animation.CrossFade("Attack"); 
					}
					
					if (mesh.animation.isPlaying == false)
					{
						mesh.animation.Play("Attack"); 
					}
					
					
					if (lastAttack > TimeBetweenAttacks)
					{
						soundPlayer.PlayOneShot (AttackSound);					
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
					//change to the walk mesh
					if (CurAnimation != AnimationsEnum.Walk)
					{
						CurAnimation = AnimationsEnum.Walk;
						mesh.animation.CrossFade ( "Walk");
						//attack.animation.Play("Take 001");							
					}
					
					if (mesh.animation.isPlaying == false)
					{
						mesh.animation.Play ( "Walk");
					}
						
					//Move towards the player
					transform.LookAt(player.transform.position);				
					transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
					rigidbody.MovePosition(transform.position + (transform.forward * MovementSpeed * Time.deltaTime) );
					Debug.Log("moved towards player");
				}			
			}
			else
			{
				//change to the walk mesh if we aren't on it
				if (CurAnimation != AnimationsEnum.Walk)
				{
					CurAnimation = AnimationsEnum.Walk;
					mesh.animation.CrossFade ("Walk");
					
					//attack.animation.Play("Take 001");							
				}
				if (mesh.animation.isPlaying == false)
				{
					mesh.animation.Play ("Walk");
				}
			
				
				lastAttack = 0f;
				Attacking = false;
				flatTransform = new Vector3(transform.position.x, 0, transform.position.z);
				flatNewPos = new Vector3(newPos.x, 0, newPos.z);
				distanceToNewPos = Vector3.Distance(flatTransform, flatNewPos);
				
				if (pointRange > distanceToNewPos)
				{
					//Find a new random position within the movement radius
					
					Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
					
					do
					{
						newPos = startPos + (Random.insideUnitSphere * MovementRadius);
						newPos = new Vector3(newPos.x,playerPos.y ,newPos.z);
					} while(Physics.Raycast(playerPos, newPos,Vector3.Distance (playerPos,newPos)));
				}
				else
				{
					//not at new position, so move some towards it.
					transform.LookAt(newPos);
					transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
					rigidbody.MovePosition(transform.position + (transform.forward * MovementSpeed * Time.deltaTime) );
					Debug.Log ("Moved towards newPos");
					
					/*
					//if (Mathf.Abs((float)(newRot.eulerAngles.y - transform.rotation.eulerAngles.y)) > 1f)
					if (newRot.eulerAngles.y != transform.rotation.eulerAngles.y)
					{
						//transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * RotateSpeed);
						transform.eulerAngles = new Vector3(0, newRot.eulerAngles.y, 0);
					}
					else
					{
						transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
						rigidbody.MovePosition(transform.position + (transform.forward * MovementSpeed * Time.deltaTime) );
					}
					
					*/
					
					
					
					
				}
			}
		}
		else
		{
			//change to the walk mesh if we aren't on it
				if (CurAnimation != AnimationsEnum.Dead)
				{
					CurAnimation = AnimationsEnum.Dead;
					mesh.animation.CrossFade ("Dead");					
					//attack.animation.Play("Take 001");							
				}
			
			if (mesh.animation.isPlaying == false)
			{
				Destroy (gameObject);
			}
			
			/*
			
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
					
					deathFlashCount += 1;
					lastDeathFlash = 0f;
				}
				else
				{
					lastDeathFlash += Time.deltaTime;					
				}
			}
			
			*/
			
		}
	}
	
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(new Vector3(newPos.x,0,newPos.z),1f);
		Gizmos.DrawLine(transform.position,newPos);
		
	
	}
	
	public void Die()
	{
		die = true;
	}
}
