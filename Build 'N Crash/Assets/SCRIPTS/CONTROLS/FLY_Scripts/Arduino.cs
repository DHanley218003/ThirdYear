using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;



public class Arduino : MonoBehaviour 
{
	private string comPort;
	private SerialPort stream ;


	public Arduino(string comPortParam)
	{
		comPort = comPortParam;
		stream = new SerialPort (comPort, 9600);
	}

	 public void Start () 
	{
		stream.ReadTimeout = 1;
		stream.Open();
		/*StartCoroutine
		(
			AsynchronousReadFromArduino
			(   (string s) => Debug.Log(s),     // Callback
				() => Debug.LogError("Error!"), // Error callback
				10f                             // Timeout (seconds)
			)
		);*/
	}

	/*
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.A))
			WriteToArduino ("1000");
		if (Input.GetKeyDown (KeyCode.D))
			WriteToArduino ("2000");
	}
*/

	public void WriteToArduino(string message) {
		stream.WriteLine(message);
		stream.BaseStream.Flush();
	}

	public string ReadFromArduino (int timeout) {
		stream.ReadTimeout = timeout;        
		try {
			return stream.ReadLine();
		}
		catch (TimeoutException) {
			return null;
		}
	}

	public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail, float timeout) {
		DateTime initialTime = DateTime.Now;
		DateTime nowTime;
		TimeSpan diff = default(TimeSpan);

		string dataString = null;

		do {
			try {
				dataString = stream.ReadLine();
			}
			catch (TimeoutException) {
				dataString = null;
			}

			if (dataString != null)
			{
				callback(dataString);
				yield return null;
			} else
				yield return new WaitForSeconds(0.05f);

			nowTime = DateTime.Now;
			diff = nowTime - initialTime;

		} while (diff.Milliseconds < timeout);

		if (fail != null)
			fail();
		yield return null;
	}
}