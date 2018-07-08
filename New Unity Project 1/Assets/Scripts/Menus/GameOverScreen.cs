using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverScreen : MonoBehaviour {

    private Text gmover, score;


	// Use this for initialization
	void Start () {
        string currscore = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDCommands>().score.text;
        gmover.text = Strings.gameover;
        score.text = Strings.score + " " + currscore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
