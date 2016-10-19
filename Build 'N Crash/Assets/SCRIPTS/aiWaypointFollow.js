#pragma strict
//Array of waypoints for the drone to follow
public var waypoints: GameObject[];
public var waypointIndex : int = 0;
public var hoverForce : float;
public var speed : float;
public var turnSpeed : float;
//the vector between drone and waypoint
private var distance : Vector3;
//The rigidbody of the drone
private var rb : Rigidbody;

function Start () 
{
	//Finds all GameObjects in the scene which have the tag "waypoint", and stores them in a list
	waypoints = GameObject.FindGameObjectsWithTag("waypoint");
	//Finds the rigidbody component attached to the GameObject with this script
	rb = GetComponent(Rigidbody);
}

function FixedUpdate () 
{
	//Moves the GameObject towards the waypoint it is currently aiming for
	moveToward(waypoints[waypointIndex]);
}

//Moves the GameOBject toward another gameObject
function moveToward( waypoint : GameObject )
{
	//Calculates the distancde between the two objects and stores it as a vector
	distance = waypoint.transform.position - transform.position;
	//Displays this vector in the scene view for clarity
	Debug.DrawRay(transform.position, distance, Color.red);

	//If the waypoint is 0.5 units higher than the GameObject or more, the GameObject will raise
	if(transform.position.y < waypoint.transform.position.y - 0.5 )
	{
		rb.AddForce(0f,hoverForce,0f);
	}
	//If the waypoint is 0.5 units lower than the GameObject or more, the GameObject will lower
	else if(transform.position.y > waypoint.transform.position.y + 0.5 )
	{
		rb.AddForce(0f,-hoverForce,0f);
	}

	//some extra force added to somewhat counteract gravity
	rb.AddRelativeForce(0f,hoverForce,0f);

	//Creates a queaternion that holds a rotation towards the waypoint
	var newRotation = Quaternion.LookRotation(distance, Vector3.up);
	//Sets the rotation to 0 on the x and z axis, because we only want to rotate on the y axis to mimic player steering
    newRotation.x = 0.0;
    newRotation.z = 0.0;
    //Changes the current rotation of the GameObject to the new one over time
    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed);

    //Moves the GameObject forward
	transform.Translate(Vector3.forward * Time.deltaTime * speed);

	//If the GameObject draws close to the waypoint, start going to the next waypoint
	if(distance.magnitude < 5)
	{
		//Waypointindex is used to access the current waypoint from the list, so incrementing it
		//means that the next waypoint will now be used instead
		waypointIndex++;
		//If the waypoint index is at the end of the list (i.e, all waypoints have been passed)
		//destroy this script (which will stop the gameObject from moving)
		if(waypointIndex >= waypoints.length)
		{
			Destroy(this);
		}
	}

	//If the GameObject is falling above a certain speed (due to gravity) apply a counterforce
	//proportional to the extra speed. This effectively limits the maximum speed the object can
	//fall, and will give the appearance of flight rather than free-falling when descending.
	if(rb.velocity.y < -2)
	{
		rb.AddForce(Vector3(0, -2 - rb.velocity.y, 0));
	}
}