using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using VRStandardAssets.Utils;

public class ControlScript : MonoBehaviour {

	int test = 0;
	public float speed = 50.0f;
	public float speed2 = 50.0f;
	public float turnSpeed = 60;
	public float distance;
	public float updateRate = 0.5f;
	public float speedTarget = 50f;
	public int speed3 = 0;
	//private float rotation_V = 0f;
	private float rotation_H = 0;
	public Arduino fan;
	public bool arduinoConnected = false;
	public string comPort = "COM5";
	public VREyeRaycaster raycastScript;
	public VRInteractiveItem target;
	//Used for bringing up menu
	public GameObject menuObj = null;
	private GameObject menu = null;


	public float getSpeed(){return speed;}
	public void setSpeed(float speed){this.speed = speed;}
	public void stealSpeed()
	{
		if (raycastScript.CurrentInteractible != null) 
		{
			Debug.Log ("object detected!");
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
		test++;
		print ("test");
		speed3 = (int) (speed * 6 + 1200);
		if (arduinoConnected)
			fan.WriteToArduino (speed3.ToString ());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (speed < speedTarget)
			speed += updateRate;
		if (speed > speedTarget)
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
		transform.Rotate(0, input*Time.deltaTime*turnSpeed*2, 0);
	}

	//Stops player and instantiates menu
	public void instantiateMenu()
	{
		if (menu == null) 
		{
			Time.timeScale = 0;
			menu = (GameObject)Instantiate (menuObj, new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z + 1.0f), Quaternion.identity);
			menu.transform.parent = transform;
		}
	}
}
