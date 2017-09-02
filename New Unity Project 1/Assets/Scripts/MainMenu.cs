using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewGame()
	{
		SceneManager.LoadScene ("Testing");
	}

	public void Options()
	{
	}

	public void Quit()
	{
	}

}
