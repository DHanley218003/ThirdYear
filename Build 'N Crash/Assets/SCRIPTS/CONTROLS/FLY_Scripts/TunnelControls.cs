using UnityEngine;
using System.Collections;

public class TunnelControls : MonoBehaviour {
	// speeds
	public float moveSpeed = 6000.0f;
	public float turnSpeed = 500.0f;
	public float power = 6000.0f;
	// objects
	public Rigidbody drone;
	public Arduino fan;
	// inputs
	float forwardInput = 0.0f;
	float sideInput = 0.0f;
	float heightInput = 0.0f;
	float turnInput = 0.0f;
	// forward speed
	public int speed;
	//used when arduino is not present to avoid constant errors
	public bool arduinoConnected = false;
	//COM ports can be different on every system, I should find a system to detect an arduino
	public string comPort = "COM5";

	void Start()
	{
		drone = GetComponent<Rigidbody>();
		if (arduinoConnected) 
		{
			fan = new Arduino (comPort);
			fan.Start ();
		}
	}
	// Update is called once per frame
	void Update () 
	{
		forwardInput = Input.GetAxis ("Vertical");
		sideInput = Input.GetAxis ("Horizontal");
		turnInput = Input.GetAxis ("Turn");
		heightInput = Input.GetAxis("Height");
		speed = (int) drone.transform.TransformDirection (drone.GetComponent<Rigidbody> ().velocity).x + 1000;
		if (arduinoConnected) 
			fan.WriteToArduino (speed.ToString ());
		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
	}

	void FixedUpdate()
	{
		//drone.AddForce (0f, 9.82f, 0f);
		if ((GetComponent<Rigidbody> ().velocity.x < 100 || sideInput < 0) && (GetComponent<Rigidbody> ().velocity.x > -100 || sideInput > 0))
			strafe (sideInput, moveSpeed);

		turn (turnInput, turnSpeed);

		height (heightInput, power);

		if ((GetComponent<Rigidbody> ().velocity.z < 100 || forwardInput < 0) && (GetComponent<Rigidbody> ().velocity.z > -100 || forwardInput > 0))
			moveForward (forwardInput, moveSpeed);
	}

	/**
	 * float input = value from 0 to 1
	 * float speed = base speed value
	 **/
	void moveForward(float input, float speed)
	{
		drone.AddRelativeForce (-(input * speed) * Time.deltaTime, 0f, 0f);
	}

	void strafe(float input, float speed)
	{
		drone.AddRelativeForce (0f, 0f, (input * speed) * Time.deltaTime);
	}

	void turn(float input, float speed)
	{
		drone.AddTorque (0f,(input * speed) * Time.deltaTime,0f);
	}

	void height(float input, float speed)
	{
		drone.AddRelativeForce (0f, ((input * speed) * Time.deltaTime), 0f);
	}
}
