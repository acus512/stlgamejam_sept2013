using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float playerSpeed = 3f;
	public float runMultiplyer = 1.5f;
	public float turnSpeed = 3f;
	public float jumpSpeed = 100f;
	public float gravityChangeSpeed = 3f;
	public GameObject camera;
	
	public bool collidesBelow = false;
	public Vector3 gravity;
	
	public Vector3 vel;
	public Vector3 curUp;
	public bool lockControls = false;
	
	// Use this for initialization
	void Start () {
		rigidbody.sleepVelocity = .1f;
		gravity = Physics.gravity;
		curUp = -transform.up;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		RaycastHit hit = new RaycastHit();
		if(!lockControls)
		{
			Time.timeScale = 1;
			//Movement (based on gameobject, not camera look for oculus rift.)
			var moveDirection = new Vector3(Input.GetAxis("Horizontal")*playerSpeed*Time.deltaTime, 0, Input.GetAxis("Vertical")*playerSpeed*Time.deltaTime);
			if(Input.GetButton("Run"))
			{
				moveDirection *= runMultiplyer;
			}
			moveDirection += (gravity)*Time.deltaTime;
			moveDirection = transform.TransformDirection(moveDirection);
			
			
			//Jump code
			if(Input.GetButtonDown("Jump") && collidesBelow)
			{
				Debug.Log("Jumpped");
				rigidbody.AddForce(transform.up * jumpSpeed);
			}
			
			//Actually apply movement
			rigidbody.velocity = moveDirection+new Vector3(0,rigidbody.velocity.y,0);
			
			//Rotation (based on gameobject, not camera look
			//Turns based on degrees per second. 3 = 3 degrees per second, 50 means the turn speed is capped at 50degrees per second.
			transform.Rotate(new Vector3(0,(Input.GetAxis("Mouse X")+Input.GetAxis("Joy X"))*turnSpeed*Time.deltaTime,0));
			camera.transform.Rotate(new Vector3((-Input.GetAxis("Mouse Y")+Input.GetAxis("Joy Y")),0f,0f));
		}else{
			Time.timeScale = 0;
		}

		//resets collision flags. 
		//NOTE: This will always make it appear false in the editor as the editor updates after the update loop,
		//but before the physics loop.
		
		if(Physics.SphereCast(transform.position+new Vector3(0,.5f,0),.45f,-transform.up,out hit,1.5f))
		{
			if(hit.transform.tag != "Player")
			{
				collidesBelow = true;
			}else{
				collidesBelow = false;
				transform.parent = null;
			}
		}else{
			collidesBelow = false;
			transform.parent = null;
		}
	}
	
	public void LockControls()
	{
		lockControls = true;
	}
	
	public void UnlockControls()
	{
		lockControls = false;
	}
}
