using UnityEngine;
using System.Collections;



public class AIBank_Script : MonoBehaviour {

	Vector3 oldLocalEulers;
	public float angle = 0.0f;
	void Start () {
		oldLocalEulers = transform.position;
	}
	

	void FixedUpdate () {

		angle = Vector3.Angle(oldLocalEulers, transform.forward);

		oldLocalEulers = transform.localPosition;

		}

		
	

}
