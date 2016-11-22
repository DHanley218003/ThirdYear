using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using VRStandardAssets.Utils;

public class ControlScript : MonoBehaviour {

	public float speed = 50.0f;
	public float speed2 = 50.0f;
	public float distance;
	public float updateRate = 0.5f;
	public int speed3 = 0;
	//private float rotation_V = 0f;
	private float rotation_H = 0;
	public Arduino fan;
	public bool arduinoConnected = false;
	public string comPort = "COM5";
	public VREyeRaycaster raycastScript;
	public VRInteractiveItem target;

	public float getSpeed(){return speed;}
	public void setSpeed(float speed){this.speed = speed;}
	public void stealSpeed()
	{
		if (raycastScript.CurrentInteractible != null) 
		{
			print ("object detected!");
			target = raycastScript.CurrentInteractible;
			if (target.CompareTag ("Enemy")) 
			{
				target.GetComponent<AgentBehaviourScript> ().setSpeed (15.0f);
				print ("object slowed!");
				setSpeed (120.0f);
			} else 
			{
				setSpeed (15.0f);
			}
		}
	}
	// Use this for initialization
	void Start () {
		raycastScript = GameObject.Find("MainCamera").GetComponent <VREyeRaycaster>();
		updateRate = 0.5f;
		speed2 = speed-1;
		if (arduinoConnected) 
		{
			fan = new Arduino (comPort);
			fan.Start ();
		}
	}

	void Update()
	{
		speed3 = (int) (speed * 6 + 1200);
		if (arduinoConnected)
			fan.WriteToArduino (speed3.ToString ());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (speed < 50)
			speed += updateRate;
		if (speed > 50)
			speed -= updateRate;
		
		float step = speed * Time.deltaTime;

		transform.position += transform.forward *  step;




		//rotation_V = Input.GetAxis("Vertical") * step;
		rotation_H = Input.GetAxis("Horizontal") * step;
		//rotateVertical(rotation_V);
		rotateHorizontal (rotation_H);
		//this byt will alway keep level in z axis
		Quaternion q = transform.rotation;
		q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
		//transform.rotation = q;

		//making sure to travel in direction of the guide element

		/*Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1.4f * Time.deltaTime, 0.0F);
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

	/*public void rotateVertical(float input)
	{
		transform.Rotate(input, 0 , 0);
	}*/

	public void rotateHorizontal(float input)
	{
		transform.Rotate(0, input*2, 0);
	}
}
