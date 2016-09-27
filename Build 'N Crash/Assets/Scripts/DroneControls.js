#pragma strict
var drone:Rigidbody;
var speed:float = 90f;
var turnSpeed:float = 5f;
var hoverForce:float = 1.5f;
var hoverHeight:float = 1f;
var powerInput:float;
var turnInput:float;
var hoverInput:float;

function Start () 
{
	drone = gameObject.GetComponent(Rigidbody) as Rigidbody;
}

function Update ()
{
	powerInput = Input.GetAxis ("Vertical");
	turnInput = Input.GetAxis ("Horizontal");
	hoverInput = Input.GetAxis ("Jump");
	
}

function FixedUpdate () 
{
	hoverHeight += hoverInput;
	if(drone.transform.position.y < hoverHeight - 3)
		drone.AddForce(0f,hoverForce,0f,ForceMode.Force);
		
	else if(drone.transform.position.y > hoverHeight + 3)
		drone.AddForce(0f,-hoverForce,0f,ForceMode.Force);
		
	//else
		//drone.AddForce(0f,0f,0f,ForceMode.Force);
	
	drone.AddRelativeForce(0f, 0f, powerInput * speed);
	drone.AddRelativeForce(0f, turnInput * turnSpeed, 0f);
}