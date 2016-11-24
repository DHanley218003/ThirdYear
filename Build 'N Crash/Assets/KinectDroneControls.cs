/*This script interprets the players movements as seen by the Kinect Wrapper Package for Unity3D,
  and calls relevant scripts from ControlScript.cs.*/

using UnityEngine;
using System.Collections;

public class KinectDroneControls : MonoBehaviour {

	float turnInput; //Stores the value used to calculate turning based on player input

	//Stores the players left and right hands. These are objects in the scene, children of KinectPlayerBody and move to match the players movements
	GameObject playerLeftHand;
	GameObject playerRightHand;


	//Another script attached to player, which contains functions for moving the player, as well as collision functions.
	ControlScript playerScript;


	void Start () 
	{
		playerLeftHand = GameObject.Find("13_Hand_Left");
		playerRightHand = GameObject.Find("23_Hand_Right");
		playerScript = GetComponent<ControlScript> ();
	}

	void FixedUpdate () 
	{
		//Calculates player turning as a float between 1 and -1 (mimicking horizontalAxis)
		turnInput = playerLeftHand.transform.position.y - playerRightHand.transform.position.y;
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

		//Calls a function in ControlScript to rotate the player based on their input
		playerScript.rotateHorizontal(turnInput);
	}
}