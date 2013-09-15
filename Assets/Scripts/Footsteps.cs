using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {
	
	public CharacterController controller;
	public AudioClip[] normalStep;
	public AudioClip[] waterStep;
	
	private bool step = true;
	private float audioStepLengthWalk = 0.45f;
	private float audioStepLengthRun = 0.25f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Determines what kind of terrain we are on
	void OnControllerColliderHit ( ControllerColliderHit hit)
	{
		if (controller.isGrounded && controller.velocity.magnitude < 7.0f && controller.velocity.magnitude > 5.0f && hit.gameObject.tag == "Untagged" && step == true )
		{
			WalkOnDry();
		}
		else if (controller.isGrounded && controller.velocity.magnitude > 8.0f && hit.gameObject.tag == "Untagged" && step == true)
		{
			RunOnDry();
		}
		else if (controller.isGrounded && controller.velocity.magnitude < 7.0f && controller.velocity.magnitude > 5.0f && hit.gameObject.tag == "Water" && step == true )
		{
			WalkOnWet();
		}
		else if (controller.isGrounded && controller.velocity.magnitude > 8.0f && hit.gameObject.tag == "Water" && step == true)
		{
			RunOnWet();
		}
	}
	
	IEnumerator WaitForFootSteps(float stepsLength)
	{
		step = false;
		yield return new WaitForSeconds(stepsLength);
		step = true;
	}
	
	private void WalkOnDry()
	{
		audio.clip = normalStep[Random.Range(0, (int)normalStep.Length)];
		audio.volume = 0.1f;
		audio.Play();
		StartCoroutine(WaitForFootSteps(audioStepLengthWalk));
	}
	
	private void RunOnDry()
	{
		audio.clip = normalStep[Random.Range(0, (int)normalStep.Length)];
		audio.volume = 0.3f;
		audio.Play();
		StartCoroutine(WaitForFootSteps(audioStepLengthRun));
	}
	
	private void WalkOnWet()
	{
		audio.clip = waterStep[Random.Range(0, (int)waterStep.Length)];
		audio.volume = 0.1f;
		audio.Play();
		StartCoroutine(WaitForFootSteps(audioStepLengthWalk));
	}
	
	private void RunOnWet()
	{
		audio.clip = waterStep[Random.Range(0, (int)waterStep.Length)];
		audio.volume = 0.3f;
		audio.Play();
		StartCoroutine(WaitForFootSteps(audioStepLengthRun));
	}
}
