#pragma strict

public var player:GameObject;
public var speed:UI.Text;
var valueOfSpeed:int = 0;
var lastPosition:Vector3;

function Start () {

}

function FixedUpdate () {
speed.text = (valueOfSpeed).ToString();
checkSpeed();
}


function checkSpeed()
{
	valueOfSpeed = (((player.transform.position - lastPosition).magnitude) / Time.deltaTime) ;
     lastPosition = player.transform.position;
}