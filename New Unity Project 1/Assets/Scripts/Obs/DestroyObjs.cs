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
            obsscript.spawner.GetComponent<Spawner>().Remove(other.gameObject);
            HUD.GetComponent<HUDCommands>().ScoreIncrease(obsscript.scorefactor);
        }
    }
}
