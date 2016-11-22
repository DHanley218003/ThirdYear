using UnityEngine;
using System.Collections;
using System.Linq;


public class AgentBehaviourScript : MonoBehaviour {
	public float speed = 50.0f;
	public float turnSpeed = 3.0f;
	public string [] waypointNames;
	public Vector3 [] waypoints;
	public Vector3 currentWaypoint;
	public int wp_count = 0;
	public float updateRate = 0.5f;
	//Quaternion rot;
	public int waypointIndex = 0;


	// Initialization
	void Start ()
	{
		//rot = transform.rotation;
		initWaypointArray ();
		currentWaypoint = waypoints [waypointIndex];
	}

	public void setSpeed(float speed){
		this.speed = speed;
	}

	// Update is called once per frame
	void Update ()
	{
		moveToNextWaypoint ();

	}

	void FixedUpdate()
	{
		if (speed < 50)
			speed += updateRate;
		if (speed > 50)
			speed -= updateRate;
	}

	//##################################################################

	void initWaypointArray()
	{
		GameObject[] allTheWaypoints;

		allTheWaypoints= GameObject.FindGameObjectsWithTag ("Waypoint").OrderBy(go => go.name).ToArray();

		wp_count = allTheWaypoints.Length;


		if (wp_count == 0) {
			//Debug.Log ("No objects with a tag - waypoint");
		} else {
			waypoints = new Vector3 [wp_count];

			for (int i = 0; i < wp_count; i++) {
				//print(allTheWaypoints [i].gameObject.name);
				waypoints [i] = allTheWaypoints [i].transform.position;
			}
		}
	}

	void moveToNextWaypoint()
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, step);
		Vector3 targetDir = currentWaypoint - transform.position;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed * Time.deltaTime, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);

		if (1 > Vector3.Distance (this.transform.position, currentWaypoint))
		{
			currentWaypoint = waypoints[waypointIndex++];				
		}			
		if (waypointIndex == wp_count) {waypointIndex = 0;}	
	}
	public float getSpeed()
	{
		return speed;
	}

	//*******************************************************



}
