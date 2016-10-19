using UnityEngine;
using System.Collections;

public class PlayerFollowControlls_Script : MonoBehaviour {

	public GameObject guide;
	public float speed = 50.0f;
	public float speed2 = 50.0f;
	private float rotation_V = 0f;
	private float rotation_H = 0;

	// Use this for initialization
	void Start () {

		guide = GameObject.Find ("PlayerGuide");
		speed= guide.GetComponent<GuideOnRails_Script>().speed;
		speed2 = speed-1;	
	}
	
	// Update is called once per frame
	void Update () {

		float step = speed * Time.deltaTime;

		transform.position += transform.forward *  step;




		rotation_V = Input.GetAxis("Vertical") * step;
		rotation_H = Input.GetAxis("Horizontal") * step;
		transform.Rotate(rotation_V, 0 , 0);
		transform.Rotate(0, rotation_H, 0);
		//this byt will alway keep level in z axis
		Quaternion q = transform.rotation;
		q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
		//transform.rotation = q;

		//making sure to travel in direction of the guide element
		Vector3 targetDir = guide.transform.position - transform.position;

		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1.4f * Time.deltaTime, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);

		if ((targetDir.magnitude) > 20) {
			transform.rotation = Quaternion.LookRotation (newDir);
			speed = 56;
		} 
		if ((targetDir.magnitude) < 10) {
			
			speed = speed2;
		} 




		/*
		transform.position = Vector3.MoveTowards(transform.position, guide.transform.position, step);
		Vector3 targetDir = guide.transform.position - transform.position;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 2 * Time.deltaTime, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		//if(Mathf.Abs(newDir.y - transform.rotation.y) > 5)
		transform.rotation = Quaternion.LookRotation(newDir);
	*/
	}
}
