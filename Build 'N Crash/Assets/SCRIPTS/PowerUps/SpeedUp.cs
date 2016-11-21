using UnityEngine;
using System.Collections;

public class SpeedUp : MonoBehaviour {
	//Used to store drone controls script for use below.
	public Powerups player;
	public ControlScript playercs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision other)
	{
		//print ("Test");
		//if the game object is called "Player", get a reference to the script "Drone Controls"  save in player and then increase speed and print it.
		if (other.gameObject.name == "PLAYER") {
			player = other.gameObject.GetComponent<Powerups>();
			if(player.powerup == 0)
				player.powerup = 1;
			// testing speed boost on contact
			//playercs = other.gameObject.GetComponent<ControlScript>();
			//playercs.setSpeed (120.0f);
			//print (player.speed);
			Destroy (this.gameObject);
		}
	}
 
}
