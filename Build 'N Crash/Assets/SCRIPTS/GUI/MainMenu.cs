using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public void quitThisShit()
	{
		Application.Quit ();
	}

	public void debugThisShit()
	{
		Debug.Log ("Shit's been debugged");
	}

	public void changeScene(string sceneName)
	{
		Application.LoadLevel(sceneName);
	}
}