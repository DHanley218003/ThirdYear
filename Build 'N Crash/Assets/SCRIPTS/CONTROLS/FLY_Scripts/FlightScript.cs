using UnityEngine;
using System.Collections;

public class FlightScript : MonoBehaviour {
	
	public float AmbientSpeed = 100.0f;

    public float RotationSpeed = 100.0f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void FixedUpdate()
	{
		UpdateFunction();
	}
	
	void UpdateFunction()
    {

        Quaternion AddRot = Quaternion.identity;
        float roll = 0;
        float pitch = 0;
        float yaw = 0;
        roll = Input.GetAxis("Horizontal") * (Time.fixedDeltaTime * RotationSpeed);
        pitch = Input.GetAxis("Vertical") * (Time.fixedDeltaTime * RotationSpeed);
        yaw = Input.GetAxis("Jump") * (Time.fixedDeltaTime * RotationSpeed);
        AddRot.eulerAngles = new Vector3(-pitch, yaw, -roll);
        GetComponent<Rigidbody>().rotation *= AddRot;
        Vector3 AddPos = Vector3.forward;
        AddPos = GetComponent<Rigidbody>().rotation * AddPos;
        GetComponent<Rigidbody>().velocity = AddPos * (Time.fixedDeltaTime * AmbientSpeed);
    }
}
