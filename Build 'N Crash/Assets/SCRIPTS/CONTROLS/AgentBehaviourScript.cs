using UnityEngine;
using System.Collections;
using System.Linq;


public class AgentBehaviourScript : MonoBehaviour
{
	public float speed = 50.0f;
	public float turnSpeed = 5.0f;
	public float updateRate = 0.5f;
	public float speedTarget = 50f;
	public string[] waypointNames;
	public Vector3[] waypoints;
	public Vector3 currentWaypoint;
	public int wp_count = 0;
	//Quaternion rot;
	public int waypointIndex = 0;
	public float distance = 0;

	// Initialization
	void Start ()
	{
		//rot = transform.rotation;
		initWaypointArray ();
		currentWaypoint = waypoints [waypointIndex];
	}

	public void setSpeed (float speed)
	{
		this.speed = speed;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		moveToNextWaypoint ();
		if (speed < speedTarget)
			speed += updateRate;
		if (speed > speedTarget)
			speed -= updateRate;
		if (speed == speedTarget)
			speed += rnd ();
	}

	//#####  #####  ########  ########  ######  #######

	void initWaypointArray ()
	{
		GameObject[] allTheWaypoints;

		allTheWaypoints = GameObject.FindGameObjectsWithTag ("Waypoint").OrderBy (go => go.name).ToArray ();

		wp_count = allTheWaypoints.Length;


		if (wp_count == 0)
		{
			Debug.Log ("No objects with a tag - waypoint");
		}
		else
		{
			waypoints = new Vector3 [wp_count];

			for (int i = 0; i < wp_count; i++)
			{
				//print(allTheWaypoints [i].gameObject.name);
				{
					float xVec = allTheWaypoints [i].transform.position.x + rnd ();
					float yVec = allTheWaypoints [i].transform.position.y + rnd ();
					float zVec = allTheWaypoints [i].transform.position.z;

					waypoints [i] = new Vector3 (xVec, yVec, zVec);
					//waypoints [i] = allTheWaypoints [i].transform.position;
				}
			}
		}
	}


	float rnd ()
	{
		return Random.Range (-5f, 5f);
	}

	void sphereGizmo (Vector3 pos)
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere (pos, 3);
	}

	void moveToNextWaypoint ()
	{
		
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, step);
		Vector3 targetDir = currentWaypoint - transform.position;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, turnSpeed * Time.deltaTime, 0.0F);
		Debug.DrawRay (transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation (newDir);

		distance = Vector3.Distance (this.transform.position, currentWaypoint);

		if (distance < 5)
		{
			currentWaypoint = waypoints [waypointIndex++];				
		}			
		if (waypointIndex == wp_count)
		{
			waypointIndex = 0;
		}	
	}

	public float getSpeed ()
	{
		return speed;
	}

	//*******************************************************



}
