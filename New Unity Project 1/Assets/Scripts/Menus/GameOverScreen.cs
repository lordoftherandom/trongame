using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOverScreen : MonoBehaviour {

    [SerializeField]
    private Text gmover, score, restart, exit;
    [SerializeField]
    private Image backcolor;


	// Use this for initialization
	void Start () {
        string currscore = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDCommands>().score.text;
        gmover.text = Strings.gameover;
        score.text = Strings.score + " " + currscore;
        restart.text = Strings.restart;
        exit.text = Strings.exit;
	}
	
	// Update is called once per frame
	void Update () {
        if (backcolor.color.a < 1)
            backcolor.color = backcolor.color + new Color(0, 0, 0, 0.01f);
	}

    public void Restart()
    {
        SceneManager.LoadScene("Level_0");
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu-0");
    }
}
