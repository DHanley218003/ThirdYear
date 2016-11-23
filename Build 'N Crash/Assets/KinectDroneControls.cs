using UnityEngine;
using System.Collections;

public class KinectDroneControls : MonoBehaviour {

	float speed = 20f;
	float turnInput;

	GameObject playerHead;
	GameObject playerLeftHand;
	GameObject playerRightHand;
	GameObject playerLeftHip;
	GameObject playerRightHip;
	public GameObject crashEffectObj;
	ControlScript playerScript;

	void Start () 
	{
		playerHead = GameObject.Find("03_Head");
		playerLeftHand = GameObject.Find("13_Hand_Left");
		playerRightHand = GameObject.Find("23_Hand_Right");
		playerLeftHip = GameObject.Find("30_Hip_Left");
		playerRightHip = GameObject.Find("40_Hip_Right");
		playerScript = GetComponent<ControlScript> ();
	}

	void FixedUpdate () 
	{
		//Calculates player turning
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

		//Brings up menu if player makes an X shape with their arms
		if(playerLeftHand.transform.position.x > playerRightHand.transform.position.x + 0.0175 && Mathf.Abs(playerLeftHand.transform.position.y - playerRightHand.transform.position.y) < 0.05)
		{
			playerScript.pause();
		}

		//playerScript.rotateVertical(-hoverInput);
		playerScript.rotateHorizontal(turnInput);
	}


	void OnCollisionEnter(Collision collision)
	{
		ContactPoint contact = collision.contacts[0];
		Vector3 pos = contact.point;
		Instantiate(crashEffectObj, pos, new Quaternion(0,0,0,0));
	}
}