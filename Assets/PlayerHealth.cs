using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	
	private float timerSecond = 0;
	private float timer5Second = 0;
	
	// Main player health indicator
	private float health = 100.0;
	public float Health
	{
		get { return health; }
		set { health = value; }
	}
	
	// Amount of poison counters currently affecting player health
	private int poisonCounter = 0;
	public int PoisonCounter
	{
		get { return poisonCounter; }
		set { poisonCounter = value; }
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
		else if (PoisonCounter == 0 && Health < 100.0)
		{
			health += 2.5;
			if (health > 100.0)
				health = 100.0;
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
