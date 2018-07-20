using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public HUDCommands HUD;
    CircleCollider2D explosion;
    const float RADIUS_RATE = 0.2f, MAX_RADIUS = 6.0f;
	// Use this for initialization
	void Start () {
        explosion = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (explosion.radius < MAX_RADIUS)
            explosion.radius += RADIUS_RATE;
        else
            Destroy(this);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with" + gameObject.name);
        string tagOfObs = other.gameObject.tag;
        if (tagOfObs == "Points" || tagOfObs == "Health" || tagOfObs == "Obstacle" || tagOfObs == "Bomb")
        {
            Debug.Log("Bomb Hit!");
            //do effect of powerup, for now, points!
            Obstacles objScript = other.GetComponent<Obstacles>();
            HUD.ScoreIncrease(objScript.scorefactor * 0.5f);
            objScript.destroy();
        }
    }
}
