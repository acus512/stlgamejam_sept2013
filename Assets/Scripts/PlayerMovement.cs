using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float playerSpeed = 3f;
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
			transform.Rotate(new Vector3(0,Input.GetAxis("Mouse X")*turnSpeed*Time.deltaTime,0));
			camera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"),0f,0f));
		}else{
			Time.timeScale = 0;
		}
				
		//Change gravity
		if(Input.GetKeyDown(KeyCode.Q))
		{
			curUp = -curUp;
		}

		Vector3 tmpRot = Vector3.zero;
		if(curUp.y > 0)
		{
			tmpRot = Vector3.Lerp(transform.localEulerAngles, new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 180f), gravityChangeSpeed * Time.deltaTime);
		}else if (curUp.y < 0){
			tmpRot = Vector3.Lerp(transform.localEulerAngles, new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f), gravityChangeSpeed * Time.deltaTime);
		}

		tmpRot.z = Mathf.Clamp(tmpRot.z, .1f,359.9f);
		tmpRot.x = 0;
		transform.localEulerAngles = tmpRot;
		//transform.up = curUp;

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
