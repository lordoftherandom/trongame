using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public HUDCommands HUD;
    CircleCollider2D explosion;
    SpriteRenderer sprite;
    Color initColor, altColor;
    [SerializeField]
    const float SCALE_RATE = 0.8f, MAX_SCALE = 60.0f, BOMB_FLASH = 0.1f;

    private float lastFlash = 0;
	// Use this for initialization
	void Start () {
        explosion = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        initColor = sprite.color;
        altColor = Color.yellow;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 currScale = transform.localScale;
        if (currScale.x < MAX_SCALE || currScale.y < MAX_SCALE)
        {
            sprite.transform.localScale += new Vector3(SCALE_RATE, SCALE_RATE);
            if ((lastFlash += Time.deltaTime) > BOMB_FLASH)
                FlashExplosionColor();
        }
        else
        {
            sprite.enabled = false;
            Destroy(this.gameObject);
        }
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

    private void FlashExplosionColor()
    {
        lastFlash = 0;
        if (sprite.color == initColor)
            sprite.color = altColor;
        else
            sprite.color = initColor;
    }
}
