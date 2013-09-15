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
	private bool dead = false;
	private float deadDuration = 3;
	private float deadCurTime = 0;
	public Texture2D deadTexture;
	
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
		if(Input.GetButtonDown("Menu"))
		{
			lockControls = !lockControls;
			Time.timeScale = 0;
		}
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
				rigidbody.AddForce(transform.up * jumpSpeed);
			}
			
			//Actually apply movement
			rigidbody.velocity = moveDirection+new Vector3(0,rigidbody.velocity.y,0);
			
			//Rotation (based on gameobject, not camera look
			//Turns based on degrees per second. 3 = 3 degrees per second, 50 means the turn speed is capped at 50degrees per second.
			transform.Rotate(new Vector3(0,(Input.GetAxis("Mouse X")+-Input.GetAxis("JoyX"))*turnSpeed*Time.deltaTime,0));
			camera.transform.Rotate(new Vector3((-Input.GetAxis("Mouse Y")+-Input.GetAxis("JoyY"))*turnSpeed*Time.deltaTime,0f,0f));
		}else{
			//Time.timeScale = 0;
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
	
	public void Die()
	{
		dead = true;	
		lockControls = true;
	}
	
	void OnGUI()
	{
		if(dead)
		{
			deadCurTime += Time.deltaTime;
			GUI.color = new Color (0,0,0,deadCurTime/deadDuration);
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),deadTexture);
			if(deadCurTime >= deadDuration)
			{
				Application.LoadLevel(Application.loadedLevel);	
			}
		}
	}
}
