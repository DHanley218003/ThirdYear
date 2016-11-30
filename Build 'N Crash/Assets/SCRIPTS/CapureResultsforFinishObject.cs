using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public struct Result
{
	public string name;
	public double finishTime;
	public double distance;
	public Vector3 position;
}



public class CapureResultsforFinishObject : MonoBehaviour
{
	public GameObject textPainter;
	public bool colWithPlayer = false;
	public bool onlyOnce = true;
	// Use this for initialization
	public List<Result> finishedList = new List<Result> ();
	GameObject[] allTheAiRacers;
	double timeAtStart = 0;
	public string resultsOfTheRace = "";
	public SpawnLetter spl;
	public double timerLast = 0;

	void Start ()
	{
		Time.timeScale = 1;
		textPainter = this.gameObject.transform.GetChild (0).gameObject;
		timeAtStart = Time.time;
		allTheAiRacers = GameObject.FindGameObjectsWithTag ("Enemy").OrderBy (go => go.name).ToArray ();
		spl = (SpawnLetter)textPainter.GetComponent (typeof(SpawnLetter));
	}
	
	// Update is called once per frame
	void Update ()
	{
		


	}

	void OnCollisionEnter (Collision coll)
	{
		Result res = new Result ();
		res.name = coll.gameObject.name;
		res.finishTime = Time.timeSinceLevelLoad - timeAtStart;
		res.position = coll.gameObject.transform.position;
		finishedList.Add (res);
	
		timerLast = Time.timeSinceLevelLoad - timeAtStart;

		if (coll.gameObject.name.Equals ("PLAYER") && onlyOnce)
		{
			List<string> participantsNames = new List<string> ();
			List<string> finishedNames = new List<string> ();
			foreach (Result rt in finishedList)
			{
				finishedNames.Add (rt.name);
			}
			foreach (GameObject go in allTheAiRacers)
			{
				participantsNames.Add (go.name);
			}
			List<string> leftovers = participantsNames.Except (finishedNames).ToList ();
			foreach (GameObject go in allTheAiRacers)
			{
				foreach (string s in leftovers)
					if (go.name.Equals (s))
					{
						finishedList.Add (secondaryResult (go));
					}
			}
			
			if (onlyOnce)
			{	
				onlyOnce = false;
				foreach (Result r in finishedList)
				{				
					resultsOfTheRace += buldResultString (r);			
				}
				spl.testText = resultsOfTheRace;
				spl.resetLeters = true;
			}
		}

	}


	Result secondaryResult (GameObject go)
	{
		Result res2 = new Result ();
		res2.name = go.name;
		res2.finishTime = 0.0f;
		res2.distance = Vector3.Distance (transform.position, go.transform.position);
		return res2;
	}

	string  buldResultString (Result r)
	{
		string abc = "";
		if (r.finishTime > 0.1f)
		{
			abc += r.name + calcSpace (r.name) + "Time " + r.finishTime.ToString ("F2") + " _";
		}
		else
		{					
			double timeAprox = timerLast + r.distance / 31.0f;
			abc += r.name + calcSpace (r.name) + "Time " + timeAprox.ToString ("F2") + " _";
		}
		return abc;
	}

	string calcSpace (string n)
	{
		string s = "";
		for (int i = n.Length; i < 15; i++)
		{
			s += " ";
		}
		return s;
	}



}
