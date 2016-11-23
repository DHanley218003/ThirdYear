#pragma strict

var lightComponent : Light;

var aliveTime : float = 0.2f;
private var startAliveTime : float = 0.2f;


function Start()
{
	lightComponent = GetComponent(Light);
	lightComponent.intensity = 1;
}


function Update () 
{
	aliveTime -= Time.deltaTime;
	if(aliveTime > startAliveTime / 2)
	{
		lightComponent.intensity += Time.deltaTime * 20;
	}
	else
	{
		lightComponent.intensity -= Time.deltaTime * 20;
	}
	if(aliveTime <= 0)
	{
		print("destroy");
		Destroy(gameObject);
	}
}