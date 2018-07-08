using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDCommands : MonoBehaviour {

    public Text score;
    public Image[] hitpoints;
    public Sprite healthy, unhealthy;

    const int INCR_SCORE = 100;

	// Use this for initialization
	void Start () {

        foreach(Image hitpoint in hitpoints)
            hitpoint.sprite = healthy;
        score.text = "0";
	}
    
    // Update is called once per frame
    void Update()
    {

    }

    public bool HeroHit(int damage = 1)
    {
        for(int i = hitpoints.Length -1; i >= 0; i--)
        {
            if(hitpoints[i].sprite == healthy)
            {
                hitpoints[i].sprite = unhealthy;
                return true;
            }
        }
        return false;
    }
	
    public int ScoreIncrease(float scorefactor = 1)
    {
        int tempscore = int.Parse(score.text);
        float scoreincrease = scorefactor * INCR_SCORE;

        if(scoreincrease % 1 !=0)
        {
            Debug.Log("Scorefactor would create a decimial. Defaulting to 0.5");
            scoreincrease = 0.5f * INCR_SCORE;
        }

        score.text = (tempscore + (int)scoreincrease).ToString();

        return int.Parse(score.text);
    }

}
