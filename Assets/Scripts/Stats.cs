using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	private float timerSecond = 0;
	private float timer5Second = 0;
	
	// Main player health indicator
	public float health = 100.0f;
	public float Health
	{
		get { return health; }
		set { health = value;}
	}
	
	// Amount of poison counters currently affecting player health
	private int poisonCounter = 0;
	public int PoisonCounter
	{
		get { return poisonCounter; }
		set { poisonCounter = value; }
	}
	
	// Method for removing health
	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			health = 0;
			// Death condition
			SendMessage("Die",SendMessageOptions.DontRequireReceiver);
		}
	}
	
	// Method for adding health
	public void HealDamage(float heal)
	{
		if (health < 100.0f)
		{
			health += heal;
		}
		
		if (health > 100.0f)
		{
			health = 100.0f;
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timerSecond += Time.deltaTime;
		timer5Second += Time.deltaTime;
		if (timer5Second > 5.0)
		{
			if (poisonCounter > 0)
			{
				poisonCounter -= 1;
			}
			timer5Second -= 5;
		}
		if (timerSecond < 1)
			return;
		
		if (Health == 0)
		{
			// Death condition
		}
		else if (PoisonCounter == 0 && Health < 100.0f)
		{
			health += 2.5f;
			if (health > 100.0f)
				health = 100.0f;
		}
		else if (PoisonCounter > 0)
		{
			if (PoisonCounter > Health)
			{
				// Death
				Health = 0;
			}
			else
			{
				Health -= PoisonCounter;
			}
			
		}
		
		timerSecond -= 1;
	}
}
