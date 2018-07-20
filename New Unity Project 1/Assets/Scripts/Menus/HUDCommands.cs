using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDCommands : MonoBehaviour {
    public Text score;
    public Image[] hitpoints, bombs;
    public Sprite healthy, unhealthy, bombSprite;
    private Vector3 POWERSTART;
    private const int SIDELENGTH = 40;

    const int INCR_SCORE = 100;

	// Use this for initialization
	void Start () {
        foreach (Image bomb in bombs)
            bomb.color = bomb.color - new Color(0, 0, 0, bomb.color.a);

        foreach(Image hitpoint in hitpoints)
            hitpoint.sprite = healthy;
        score.text = "0";
	}
    
    // Update is called once per frame
    void Update()
    {

    }

    public int BombGained(int bomb = 1)
    {
        for(int i = 0; i < bombs.Length-1 && bomb > 0; i++)
        {
            if(bombs[i].color.a == 0 )
            {
                bombs[i].color = bombs[i].color + new Color(0, 0, 0, 1);
                bomb--;
            }
        }
        return bomb;
    }

    public bool BombUsed(int bomb = 1)
    {
        for (int i = bombs.Length - 1; i >=0; i--)
        {
            if (bombs[i].color.a == 1)
            {
                bombs[i].color = bombs[i].color - new Color(0, 0, 0, 1);
                return true;
            }
        }
            return false;
    }

    public int HPGained(int HP = 1)
    {
        for (int i = 0; i < hitpoints.Length - 1 && HP > 0; i++)
        {
            if (hitpoints[i].sprite == unhealthy)
            {
                hitpoints[i].sprite = healthy;
                HP--;
            }
        }
        return HP;
    }

    //returns true only if there are still hitpoints left (including 0)
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
