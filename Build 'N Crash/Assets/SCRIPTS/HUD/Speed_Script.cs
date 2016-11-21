using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;
using System.Collections;

public class Speed_Script : MonoBehaviour 
{
	public Text speed;
	public Text refreshRate;

	public void Start () {}

	public void FixedUpdate ()
	{
		speed.text = gameObject.GetComponent<ControlScript>().getSpeed().ToString();
		refreshRate.text = VRDevice.refreshRate.ToString();
	}
}
