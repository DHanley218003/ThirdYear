using UnityEngine;
using System.Collections;

public class BotAnimationControler : MonoBehaviour {

	Animator botAnimiator;
	public bool turningRight;
	public bool turningLeft;
	public bool normalFlight;
	public  float angle = 0;
	public float result= 0;
	public float damping = 1.0f;
	int counter = 0;
	// Use this for initialization
	void Start () {
		botAnimiator = GetComponent<Animator>();
		angle = transform.rotation.y;
		getAllTheBools ();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		result = ( transform.eulerAngles.y - angle);	

		if (counter == 5) {
			if (result > 0.35)
				setAllTheBools (true, false, false);
			else if (result < -0.35)
				setAllTheBools (false, true, false);			
			else 
				setAllTheBools (false, false, true);
			counter = 0;

		}
		counter++;
		angle = transform.eulerAngles.y;
	}

	void getAllTheBools()
	{
		turningRight = botAnimiator.GetBool ("TurnRightBody");
		turningLeft = botAnimiator.GetBool ("TurnLeftBody");
		normalFlight = botAnimiator.GetBool ("NEUTRAL BODY");

	}
	void setAllTheBools(bool r, bool l, bool n)
	{
		
		botAnimiator.SetBool ("TurnRightBody", r);
		botAnimiator.SetBool ("TurnLeftBody", l);
		botAnimiator.SetBool ("NEUTRAL BODY", n);

	}
}
