#pragma strict

//var drone:Rigidbody;
var speed:float = 20f;
/*var turnSpeed:float = 14f;
var hoverForce:float = 1.5f;
var hoverHeight:float = 5f;
var powerInput:float;*/
var turnInput:float;
var hoverInput:float;
/*var pos:float;
var tolerance:float = 1.5f;*/
var playerHead : GameObject;
var playerLeftHand : GameObject;
var playerRightHand : GameObject;
var playerLeftHip : GameObject;
var playerRightHip : GameObject;
//var playerScript : ControlScript;

function Start () 
{
	//drone = gameObject.GetComponent(Rigidbody) as Rigidbody;
	playerHead = GameObject.Find("03_Head");
	playerLeftHand = GameObject.Find("13_Hand_Left");
	playerRightHand = GameObject.Find("23_Hand_Right");
	playerLeftHip = GameObject.Find("30_Hip_Left");
	playerRightHip = GameObject.Find("40_Hip_Right");
}

function FixedUpdate () 
{
/*
	if(playerLeftHand.transform.position.z < playerLeftHip.transform.position.z && playerRightHand.transform.position.z < playerRightHip.transform.position.z)
	{
		powerInput = 1;
	}
	else if(playerLeftHand.transform.position.z > playerLeftHip.transform.position.z + 0.1 && playerRightHand.transform.position.z > playerRightHip.transform.position.z + 0.1)
	{
		powerInput = -1;
	}
	else
	{
		powerInput = 0;
	}

	if(	playerLeftHand.transform.position.x < playerLeftHip.transform.position.x && playerRightHand.transform.position.x < playerLeftHip.transform.position.x)
	{
		turnInput = 1;
	}
	else if(playerLeftHand.transform.position.x > playerRightHip.transform.position.x && playerRightHand.transform.position.x > playerRightHip.transform.position.x)
	{
		turnInput = -1;
	}
	else
	{
		turnInput = 0;
	}
	*/

/*	if(playerLeftHand.transform.position.y < playerLeftHip.transform.position.y && playerRightHand.transform.position.y < playerRightHip.transform.position.y)
	{
		hoverInput = 1;
	}
	else if(playerLeftHand.transform.position.y > playerLeftHip.transform.position.y + 0.2 && playerRightHand.transform.position.y > playerRightHip.transform.position.y + 0.2)
	{
		hoverInput = -1;
	}
	else
	{
		hoverInput = 0;
	}

	*/

	/*
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
	*/
/*	if(playerLeftHand.transform.position.y > playerHead.transform.position.y && playerRightHand.transform.position.y < playerRightHip.transform.position.y)
	{
		turnInput = 1;
	}
	else if(playerLeftHand.transform.position.y < playerLeftHip.transform.position.y && playerRightHand.transform.position.y > playerHead.transform.position.y)
	{
		turnInput = -1;
	}
	else
	{
		turnInput = 0;
	}
	*/

	turnInput = playerLeftHand.transform.position.y - playerRightHand.transform.position.y;
	//print(turnInput);
	if(turnInput > 1)
	{
		turnInput = 1;
	}
	else if(turnInput < -1)
	{
		turnInput = -1;
	}
	/*else if((turnInput > -0.05 && turnInput < 0) || (turnInput < 0.05 && turnInput > 0))
	{
		turnInput = 0;
	}
	*/

	//playerScript.rotateVertical(-hoverInput);
	//playerScript.rotateHorizontal(turnInput);
}