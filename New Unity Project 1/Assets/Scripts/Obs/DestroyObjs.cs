using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjs : MonoBehaviour {
    GameObject HUD;
	void Start () { HUD = GameObject.FindGameObjectWithTag("HUD"); }
	void Update () { }

    //Where points should be added
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Obstacles obsscript = other.gameObject.GetComponent<Obstacles>();
            HUD.GetComponent<HUDCommands>().ScoreIncrease(obsscript.scorefactor);
            obsscript.destroy();
        }
        else if(other.gameObject.tag == "Powerup")
        {
            other.gameObject.GetComponent<Obstacles>().destroy();
        }
    }
}
