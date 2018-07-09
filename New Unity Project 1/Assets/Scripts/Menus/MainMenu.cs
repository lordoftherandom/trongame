using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public Text newgame, options, quit;
	// Use this for initialization
	void Start () {
        newgame.text = Strings.newgame;
        options.text = Strings.options;
        quit.text = Strings.quit;
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
