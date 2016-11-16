using UnityEngine;
using System.Collections;

public class Powerups : MonoBehaviour 
{
	public int powerup = 2;
	float input;

	void Update()
	{
		input = Input.GetAxis ("Fire");
		if (input == 1 || Input.GetKeyDown(KeyCode.A)) 
		{
			usePowerup ();
		}
	}
	public void usePowerup()
	{
		switch(powerup)
		{
		case 0:
			break;
		case 1:
			speedBoost();
			powerup = 0;
			break;
		case 2:
			speedLaser();
			powerup = 0;
			break;
		}
	}
	public void speedBoost()
	{
		gameObject.GetComponent<ControlScript>().setSpeed (120.0f);
	}
	public void speedLaser()
	{
		gameObject.GetComponent<ControlScript> ().stealSpeed ();
	}
}
