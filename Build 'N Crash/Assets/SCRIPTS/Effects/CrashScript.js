/*This script controls a prefab consisting of a light source and a particle system.
The light source quickly brightens, then dims and is destroyed.
It is used as an effect when the player crashes*/
#pragma strict

//Stores the light component attached to this object
public var lightComponent : Light;

//Time in seconds of how long this object lasts for until destroyed
public var startAliveTime : float = 0.2f;
//Time un seconds until this object is destroyed
private var aliveTime : float = startAliveTime;


function Start()
{
	//Sets the initial intensity of the light
	lightComponent = GetComponent(Light);
	lightComponent.intensity = 1;
}


function Update () 
{
	//Counts down time until object is destroyed
	aliveTime -= Time.deltaTime;

	//If the object is in the first half of it's life cycle, gradually increase the intensity of the light
	if(aliveTime > startAliveTime / 2)
	{
		lightComponent.intensity += Time.deltaTime * 20;
	}
	//If the object is in the second half of it's life cycle, gradually decrease the intensity of the light
	else
	{
		lightComponent.intensity -= Time.deltaTime * 20;
	}

	//If the object has no time left, destroy it.
	if(aliveTime <= 0)
	{
		Destroy(gameObject);
	}
}