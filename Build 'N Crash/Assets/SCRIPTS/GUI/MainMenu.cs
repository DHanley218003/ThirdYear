using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public void Update()
	{
	}
	public void quit()
	{
		Application.Quit ();
	}

	public void debug()
	{
		Debug.Log ("A button was pressed!");
	}

	public void changeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}