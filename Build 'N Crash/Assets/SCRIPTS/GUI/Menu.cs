using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour 
{
	
	public int timeToHover = 300;
	public int counter = 0;
	public void FixedUpdate()
	{
		checkIfHover ();
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

	public void checkIfHover()
	{
		RaycastHit hit;
		Transform cam = Camera.main.transform;
		Ray ray = new Ray(cam.position, cam.forward);
		Debug.DrawRay (cam.position, cam.forward * 100, Color.black);
		if (Physics.Raycast (ray, out hit, 500)) 
		{
			if (hit.collider.tag == "Start") {
				if (counter >= timeToHover)
					changeScene ("Colm Controls Attempt");
				else
					counter++;
			} else if (hit.collider.tag == "Options") {
				if (counter >= timeToHover)
					debug();
				else
					counter++;
			} else if (hit.collider.tag == "Quit") {
				if (counter >= timeToHover)
					quit();
				else
					counter++;
			} else
				counter = 0;
		}
	}
}