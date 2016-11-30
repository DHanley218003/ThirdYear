using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
    public GameObject FinishedLogo;
    public GameObject player;
    public MusicScript music;

    private bool areWeThereYet = false;
    private bool finished = false;
    private Vector3 playerPosition;


	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        music = GameObject.Find("Music Controller").GetComponent<MusicScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Finished!!");
            finished = true;
            music.playFinishMusic();
        }
    }

    void OnGUI()
    {
        if (finished && !areWeThereYet)
        {
            areWeThereYet = true;
            playerPosition = (player.transform.position + new Vector3(0, 0, 10));
            Instantiate(FinishedLogo, playerPosition, transform.rotation);
        }

        if (finished)
        {
            //GUI.Label (Rect (x, y, width, height), message);
            //GUI.Box (Rect ((Screen.width / 2) - 256, (Screen.height / 2) - 64, 512, 128), finishedLogo);
            Time.timeScale = 0;

            if (GUI.Button(new Rect((Screen.width / 2) - 150, (Screen.height / 2) + 100, 300, 40), "Restart Game"))
            {
                //Change this to the Current Scene
                SceneManager.LoadScene(2);
                //Time.timeScale = timeTimeScale;
            }
        }
    }
}
