using UnityEngine;
using System.Collections;


public class GuideOnRails_Script : MonoBehaviour {
	public float speed = 50.0f;
	public string [] waypointNames =new string[6]{"wp01","wp02","wp03","wp04","wp05","wp06"};
	public Vector3 [] waypoints = new Vector3[6];
	public Vector3 currentWaypoint;

	public int waypointIndex = 1;


	// Initialization
	void Start ()
	{
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
		for (int i = 0; i < 6; i++) {
			waypoints[i] = GameObject.Find (waypointNames [i]).transform.position;
		}
	}

	void moveToNextWaypoint()
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, step);
		Vector3 targetDir = currentWaypoint - transform.position;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 3 * Time.deltaTime, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);

		if (1 > Vector3.Distance (this.transform.position, currentWaypoint))
		{
			currentWaypoint = waypoints[waypointIndex++];				
		}			
		if (waypointIndex == 6) {waypointIndex = 0;}	
	}
	public float getSpeed()
	{
		return speed;
	}
}
 