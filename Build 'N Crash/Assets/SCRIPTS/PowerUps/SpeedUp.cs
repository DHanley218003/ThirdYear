using UnityEngine;
using System.Collections;

public class SpeedUp : MonoBehaviour {
	//Used to store drone controls script for use below.
	public PlayerFollowControlls_Script player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision other)
	{
		print ("Test");
		//if the game object is called "Player", get a reference to the script "Drone Controls"  save in player and then increase speed and print it.
		if (other.gameObject.name == "PLAYER") {
			player = other.gameObject.GetComponent<PlayerFollowControlls_Script>();
			player.speed += 10;
			print (player.speed);
			Destroy (this.gameObject);
		}
	}
 
}
