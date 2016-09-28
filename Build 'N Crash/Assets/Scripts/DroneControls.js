#pragma strict
var drone:Rigidbody;
var speed:float = 20f;
var turnSpeed:float = 14f;
var hoverForce:float = 1.5f;
var hoverHeight:float = 5f;
var powerInput:float;
var turnInput:float;
var hoverInput:float;
var pos:float;
var tolerance:float = 1.5f;

function Start () 
{
	drone = gameObject.GetComponent(Rigidbody) as Rigidbody;
}

function Update ()
{
	powerInput = Input.GetAxis ("Vertical");
	turnInput = Input.GetAxis ("Horizontal");
	hoverInput = Input.GetAxis ("Jump");
	
	pos = drone.transform.position.y;
}

function FixedUpdate () 
{
	hoverHeight += hoverInput;
	if(drone.transform.position.y < hoverHeight - tolerance)
	{
		drone.AddForce(0f,hoverForce,0f);
	}
	else if(drone.transform.position.y > hoverHeight + tolerance)
	{
		drone.AddForce(0f,-hoverForce,0f);
	}
	//else
		//drone.AddForce(0f,0f,0f,ForceMode.Force);
	drone.AddRelativeForce(0f,hoverForce,0f);
	drone.AddRelativeForce(-(powerInput * speed), 0f, 0f);
	drone.AddRelativeForce(0f, 0f, turnInput * turnSpeed);
	if(Input.GetKey(KeyCode.Q))
		drone.AddRelativeTorque(0f, -turnSpeed, 0f);
	if(Input.GetKey(KeyCode.E))
		drone.AddRelativeTorque(0f, turnSpeed, 0f);
}