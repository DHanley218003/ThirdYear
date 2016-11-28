using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using VRStandardAssets.Utils;

public class ControlScript : MonoBehaviour {
	public float speed = 50.0f;
	public float speed2 = 50.0f;
	public float turnSpeed = 60;
	public float distance;
	public float updateRate = 0.5f;
	public float speedTarget = 50f;
	public int speed3 = 0;
	//private float rotation_V = 0f;
	private float rotation_H = 0;
	public Arduino fan;
	public bool arduinoConnected = false;
	public string comPort = "COM5";
	public VREyeRaycaster raycastScript;
	public VRInteractiveItem target;
	//Used for bringing up menu

	public GameObject menuObj = null;
	private GameObject menu = null;

	//Prefab with particleSystem to instantiate when player is going above a certain speed
	public GameObject speedEffectObj;
	//Stores an instantiated Object of the above prefab
	private GameObject speedEffect; 
	//Stores the particleSystem component of the above object
	private ParticleSystem speedParticles; 
	//A prefab containing a light source and particle system to create sparks when the player collides
	public GameObject sparkEffectObj;

	//Time in seconds between instances of sparkEffectObj being created
	public float timeBetweenSparks;
	//Keeps track of time until next spark is instantiated
	private float sparkTimer;

	//A grinding sound effect, to play during collisions
	public AudioSource grindSound;

	private int collisionCount; //Counts the number of collisions the object is currently in


	public float getSpeed(){return speed;}
	public void setSpeed(float speed){this.speed = speed;}
	public void stealSpeed()
	{
		if (raycastScript.CurrentInteractible != null) 
		{
			target = raycastScript.CurrentInteractible;
			if (target.CompareTag ("Enemy")) 
			{
				target.GetComponent<AgentBehaviourScript> ().setSpeed (15.0f);
				print ("object slowed!");
				setSpeed (120.0f);
			} else 
			{
				setSpeed (15.0f);
			}
		}
	}
	// Use this for initialization
	void Start () {
		Debug.Log (Time.timeScale);
		raycastScript = GameObject.Find("MainCamera").GetComponent <VREyeRaycaster>();
		updateRate = 0.5f;
		speed2 = speed-1;
		if (arduinoConnected) 
		{
			fan = new Arduino (comPort);
			fan.Start ();
		}

		collisionCount = 0; //Assumes player does not start in a collision
		speedEffect = null;//Ensures speedEffect is initialised to null
	}

	void Update()
	{
		speed3 = (int) (speed * 6 + 1200);
		if (arduinoConnected)
			fan.WriteToArduino (speed3.ToString ());

		//If speed is above a certain level, and there is no speedEffect, create one as a child of the player, and adjust it's position and rotation to face the player from in front.
		if (speed > 70 && speedEffect == null) {
			speedEffect = (GameObject)Instantiate (speedEffectObj, transform.position, transform.rotation);
			speedParticles = speedEffect.GetComponent<ParticleSystem>();
			speedEffect.transform.Translate(new Vector3(0,0,46));
			speedEffect.transform.Rotate(new Vector3(0,180,0));
			speedEffect.transform.parent = transform;
		} 
		//When speed falls below a certain value, and there is a speedEffect, stop it and destroy it after a brief delay
		else if (speed < 70 && speedEffect != null) {
			speedParticles.Stop();
			Destroy (speedEffect, 0.5f);//A brief delay between stopping and destruction allows all particles to pass the player, rather than have them disappear instantly
			speedEffect = null;//Ensure speedEffect is reset to null
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (speed < speedTarget)
			speed += updateRate;
		if (speed > speedTarget)
			speed -= updateRate;
		
		float step = speed * Time.deltaTime;

		transform.position += transform.forward *  step;




		//rotation_V = Input.GetAxis("Vertical") * step;
		rotation_H = Input.GetAxis("Horizontal") * step;
		//rotateVertical(rotation_V);
		rotateHorizontal (rotation_H);
		//this byt will alway keep level in z axis
		Quaternion q = transform.rotation;
		q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
		//transform.rotation = q;

		//making sure to travel in direction of the guide element

		/*Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1.4f * Time.deltaTime, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);

		if ((targetDir.magnitude) > 20) {
			transform.rotation = Quaternion.LookRotation (newDir);
			speed = 56;
		} 
		if ((targetDir.magnitude) < 10) {
			
			speed = speed2;
		} 




		/*
		transform.position = Vector3.MoveTowards(transform.position, guide.transform.position, step);
		Vector3 targetDir = guide.transform.position - transform.position;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 2 * Time.deltaTime, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		//if(Mathf.Abs(newDir.y - transform.rotation.y) > 5)
		transform.rotation = Quaternion.LookRotation(newDir);
	*/
	}

	/*public void rotateVertical(float input)
	{
		transform.Rotate(input, 0 , 0);
	}*/

	//Used to rotate the player based on player input
	public void rotateHorizontal(float input)
	{
		transform.Rotate(0, input*Time.deltaTime*turnSpeed*2, 0);
	}

	//Stops player and instantiates menu
	public void pause()
	{
		Time.timeScale = 0;

	}
	public void resume()
	{
		Time.timeScale = 30;
	}


	void OnCollisionEnter(Collision collision)
	{
		/*update collisionCount, unless the collision is a powerUp
		 * powerUps are destroyed on contact, and will NOT have an OnCollisionExit() call
		 * so we ignore them here*/
		if (collision.gameObject.tag != "PowerUp")
		{
			collisionCount++;
		}
		ContactPoint contact = collision.contacts[0];//Gets the point of contact (only a single one) of the collision
		Vector3 pos = contact.point;
		Instantiate(sparkEffectObj, pos, new Quaternion(0,0,0,0));//Instantiates a sparking effect at the point of contact
		sparkTimer = timeBetweenSparks;//Sets a timer until the next spark (used in OnCollisionStay())
	}

	void OnCollisionStay(Collision collision)
	{
		//If it is time for the next spark, instantiate one at the point of contact
		if (sparkTimer == 0) 
		{
			ContactPoint contact = collision.contacts[0];
			Vector3 pos = contact.point;
			Instantiate(sparkEffectObj, pos, new Quaternion(0,0,0,0));
			sparkTimer = timeBetweenSparks;//Resets the timer after instantiating a spark
		}

		//Counts down the timer
		sparkTimer -= Time.deltaTime;

		//Ensures the timer is never a negative number
		if (sparkTimer < 0) 
		{
			sparkTimer = 0;
		}

		//If the player collides with any object other than a powerup, play the grind sound effect if it isn't already playing
		if (!grindSound.isPlaying && collision.gameObject.tag != "PowerUp") 
		{
			grindSound.Play();
		}
	}
		
	void OnCollisionExit(Collision collision)
	{
		collisionCount--;//update collisionCount

		//If the player is currently in 0 collisions, stop playing grind sound effect if it is playing
		if (collisionCount == 0 && grindSound.isPlaying) 
		{
			grindSound.Stop();
		}
	}
}
