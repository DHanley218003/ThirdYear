//A script to check if the player is in position to the play the game.
//It compares the heights of the players hands and one shoulder to
//see if they are in a T pose, then loads the level.

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerConfirmation : MonoBehaviour {

	public int indexOfLevelToLoad;
	GameObject playerLeftHand;
	GameObject playerRightHand;
	GameObject playerLeftShoulder;
	GameObject playerRightShoulder;
	float leftHandY;
	bool inPosition;

	void Start () 
	{
		playerLeftHand = GameObject.Find("13_Hand_Left");
		playerRightHand = GameObject.Find("23_Hand_Right");
		playerLeftShoulder = GameObject.Find("10_Shoulder_Left");
		inPosition = false;
	}

	//Checks if the player is in a T pose, with +-0.1 acceptance
	void Update () 
	{
		leftHandY = playerLeftHand.transform.position.y;
		if (leftHandY > 0 //To ensure default position does not count as inPosition
		   &&leftHandY > playerLeftShoulder.transform.position.y - 0.1
		   && leftHandY > playerRightHand.transform.position.y - 0.1
		   && leftHandY < playerLeftShoulder.transform.position.y + 0.1
		   && leftHandY < playerRightHand.transform.position.y + 0.1) 
		{
			inPosition = true;
		}

		if (inPosition) 
		{
			SceneManager.LoadScene(indexOfLevelToLoad);
		}
	}
}
