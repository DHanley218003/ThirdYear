/*This script controls the music in the game.
 * It ensures the right music is playing in the right scenes.*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour {

	//Audiosources for the three different songs
	public AudioSource menuMusic;
	public AudioSource raceMusic;
	public AudioSource finishMusic;

	void Start () 
	{
		DontDestroyOnLoad (this);//Ensures this object persists between scenes
		SceneManager.sceneLoaded += UpdateMusic;//Ensures that UpdateMusic() gets called each time a scene is loaded
	}
		
	void OnDestroy()
	{
		SceneManager.sceneLoaded -= UpdateMusic;//If this object is destroyed, it will no longer be notified when a scene is loaded
	}

	/*Changes the music if it is incorrect for the scene
	 * Assumes that scenes 0 and 1 are menus, and scene 2 is the actual race*/
	void UpdateMusic(Scene scene, LoadSceneMode mode)
	{
		//If it is the first or second scene, play menuMusic and nothing else
		if (scene.buildIndex == 0 || scene.buildIndex == 1) 
		{
			if (!menuMusic.isPlaying) 
			{
				menuMusic.Play();
			}

			if (finishMusic.isPlaying) 
			{
				finishMusic.Stop();
			}

			if (raceMusic.isPlaying) 
			{
				raceMusic.Stop();
			}
		}
			
		//If it is the third scene, play raceMusic and nothing else
		else if (scene.buildIndex == 2) 
		{
			if (menuMusic.isPlaying) 
			{
				menuMusic.Stop();
			}

			if (finishMusic.isPlaying) 
			{
				finishMusic.Stop();
			}

			if (!raceMusic.isPlaying) 
			{
				raceMusic.Play();
			}
		}
	}
}
