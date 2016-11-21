using UnityEngine;
using System.Collections;
using System.Linq;


public class AgentBehaviourScript : MonoBehaviour {
	public float speed = 5.0f;
	public float turnSpeed = 1.0f;
	public string [] waypointNames;
	public Vector3 [] waypoints;
	public Vector3 currentWaypoint;
	public int wp_count = 0;
	Quaternion rot;
	public int waypointIndex = 0;


	// Initialization
	void Start ()
	{
		rot = transform.rotation;
		initWaypointArray ();
		currentWaypoint = waypoints [waypointIndex];
	}



	// Update is called once per frame
	void Update ()
	{
		moveToNextWaypoint ();

	}

	//##################################################################

	void initWaypointArray()
	{
		GameObject[] allTheWaypoints;

		allTheWaypoints= GameObject.FindGameObjectsWithTag ("Waypoint").OrderBy(go => go.name).ToArray();

		wp_count = allTheWaypoints.Length;


		if (wp_count == 0) {
			Debug.Log ("No objects with a tag - waypoint");
		} else {
			waypoints = new Vector3 [wp_count];

			for (int i = 0; i < wp_count; i++) {
				print(allTheWaypoints [i].gameObject.name);
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
