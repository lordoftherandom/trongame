using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public Text newgame, instructions;
    public GameObject tutorial;
	// Use this for initialization
	void Start () {
        newgame.text = Strings.newgame;
        instructions.text = Strings.instructions;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewGame()
    { 
		SceneManager.LoadScene ("Level_0");
	}

	public void Options()
	{
	}

    public void Instructions()
    {
        tutorial.SetActive(true);
        this.gameObject.SetActive(false);
    }

	public void Quit()
	{
        Application.Quit();
	}

}
