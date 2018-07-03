using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	public Canvas pauseMenu;
	private GameObject map;
	// Use this for initialization
	void Start () {
		pauseMenu.GetComponent<Canvas> ().enabled = false;
		map = GameObject.FindGameObjectWithTag ("Parent");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel") && pauseMenu.GetComponent<Canvas> ().enabled == false) {
			map.GetComponent<Map> ().HideObs ();
			Time.timeScale = 0.0f;
			pauseMenu.GetComponent<Canvas> ().enabled = true;
		} else if (Input.GetButtonDown("Cancel") && pauseMenu.GetComponent<Canvas> ().enabled == true) {
			Time.timeScale = 1.0f;
			map.GetComponent<Map> ().UnHideObs ();
			pauseMenu.GetComponent<Canvas> ().enabled = false;
		}
		}


	public void Restart()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void Quit()
	{
		
	}

	public void MainMenu()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene ("MainMenu-0");
	}

}
