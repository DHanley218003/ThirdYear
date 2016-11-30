#pragma strict
import UnityEngine.SceneManagement;

public var FinishedLogo:GameObject;
public var player:GameObject;

private var areWeThereYet:boolean = false;
private var finished:boolean = false;
private var playerPosition:Vector3;

function Start ()
{
	Time.timeScale = 1;
}

function OnTriggerEnter ()
{
	Debug.Log("Finished!!");
	finished = true;
}

function OnGUI ()
{
	if (finished && !areWeThereYet)
	{
		areWeThereYet = true;
		playerPosition = (player.transform.position + new Vector3 (0, 0, 10));
		Instantiate(FinishedLogo, playerPosition, transform.rotation);
	}

	if (finished)
	{
		//GUI.Label (Rect (x, y, width, height), message);
		//GUI.Box (Rect ((Screen.width / 2) - 256, (Screen.height / 2) - 64, 512, 128), finishedLogo);
		Time.timeScale = 0;

		if (GUI.Button (Rect ((Screen.width / 2) - 150, (Screen.height / 2) + 100, 300, 40), "Restart Game"))
		{
			//Change this to the Current Scene
			SceneManager.LoadScene("Colm Controls Attempt");
			//Time.timeScale = timeTimeScale;
		}
	}
}