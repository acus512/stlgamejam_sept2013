using UnityEngine;
using System.Collections;

public class ArrowShooter : MonoBehaviour {

	static public ArrowShooter Instance;
	public Arrow arrowPrefab;

	public float arrowCooldown;
	public float minImpulse;
	public float maxImpulse;
	
	float arrowTimer;
	

	void Awake()
	{
		if (Instance == null) {
			Instance = this;
		}
		else {
			Debug.Log("ArrowShooter already exists!");
		}
	}

	// Use this for initialization
	void Start () {
		if (arrowPrefab == null) {
			Debug.LogError("No arrow prefab exists for ArrowShooter " + gameObject.name);
			return;
		}
	}

	//amountCharged should be from 0 to 1
	public void FireArrow(float amountCharged)
	{
		if (arrowTimer > 0) {
			Debug.Log("Arrow isn't ready to fire yet.");
			return;
		}

		Arrow newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation) as Arrow;
		if (newArrow == null) {
			Debug.LogError("Couldn't instantiate arrow!");
			return;
		}

		arrowTimer += arrowCooldown;
		float impulse = Mathf.Lerp(minImpulse, maxImpulse, amountCharged);
		newArrow.rigidbody.AddForce(impulse * newArrow.transform.forward, ForceMode.Impulse);
	}


	void Update()
	{
		if (arrowTimer > 0) {
			arrowTimer -= Time.deltaTime;
		}
	}
}
