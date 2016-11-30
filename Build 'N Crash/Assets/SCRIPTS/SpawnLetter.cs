using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class SpawnLetter : MonoBehaviour
{
	public  GameObject[] alphaPrefabs;
	public  int[] encodedText;
	public string testText = "0123456789_ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public bool resetLeters = false;
	bool makeSomeLetters = false;

	[Range (0.0f, 2.0f)]
	public float fontSize = 1;
	[Range (0.0f, 2.0f)]
	public float vertPadding = 0.3f;
	public float nextLinePosition = 0;
	[Range (0.0f, 2.0f)]
	public float fontHorSpcaing = 1;

	// Use this for initialization
	void Start ()
	{
		alphaPrefabs = Resources.LoadAll ("ALPHABET").Cast<GameObject> ().OrderBy (go => go.name).ToArray ();
		deleteAllLetters ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		resetAllLetters ();	
	}

	public int [] mapText (string text)
	{
		text = text.ToUpper ();
		encodedText = new int[text.Length];
		//mapping cahracters int values to the array of 3d characters.
		for (int i = 0; i < text.Length; i++)
		{
			if (System.Char.IsLetter (text [i]))
			{
				encodedText [i] = (int)text [i] - 55;
			}
			else if (text [i] == '.')
			{
				encodedText [i] = 36;
			}
			else if (System.Char.IsDigit (text [i]))
			{
				encodedText [i] = (int)text [i] - 48;
			}
			else if (text [i] == '_')
			{
				encodedText [i] = 100;//NEW LINE
			}
			else if (System.Char.IsWhiteSpace (text [i]))
			{
				encodedText [i] = 99;//SPACE
			}
		}
		return encodedText;
	}

	public void spawnText (int[] encodedText)
	{
		float charPos = 0.0f;
		for (int i = 0; i < encodedText.Length; i++)
		{
			if (encodedText [i] <= 36 && encodedText [i] >= 0)
			{
				Vector3 newPostion = new Vector3 (transform.position.x + charPos, transform.position.y + nextLinePosition, transform.position.z);
				Vector3 scale = new Vector3 (fontSize, fontSize, fontSize);
				alphaPrefabs [encodedText [i]].transform.localScale = scale;
				Instantiate (alphaPrefabs [encodedText [i]], newPostion, Quaternion.identity);
				charPos += fontHorSpcaing;
			}
			else if (encodedText [i] == 100)//NEW LINE
			{
				nextLinePosition -= fontSize + vertPadding;
				charPos = 0.0f;
			}
			else if (encodedText [i] == 99)//SPACE
			{
				charPos += fontHorSpcaing;
			}


		}
	}

	public void deleteAllLetters ()
	{
		GameObject[] allTheLetters;

		allTheLetters = GameObject.FindGameObjectsWithTag ("Alphabet").OrderBy (go => go.name).ToArray ();
		for (int i = 0; i < allTheLetters.Length; i++)
		{
			foreach (GameObject go in allTheLetters)
			{
				Destroy (go);
			}		
		}
		nextLinePosition = 0;
	}

	public void resetAllLetters ()
	{
		if (resetLeters)
		{
			deleteAllLetters ();
			resetLeters = false;
			makeSomeLetters = true;
		}
		if (makeSomeLetters)
		{

			spawnText (mapText (testText));
			makeSomeLetters = false;
		}
	}
}


