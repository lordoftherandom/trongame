using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHit : MonoBehaviour {

	public float transTimeMax;
	public float invicTime;
	private bool collHappened;
	private float curTransTime;
	private float collTime;
    private Renderer heroSkin;
    private GameObject HUD;

	void Start () {
		collHappened = false;
		curTransTime = 0.0f;
        heroSkin = gameObject.GetComponent<Renderer>();
        HUD = GameObject.FindGameObjectWithTag("HUD");
	}
	

	void Update () {
        if (collHappened)
            FlashSkin();
	}

    void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Obstacle") {
			if (!collHappened)
				StartCoroutine (noHurtMe());
		}
    }

	IEnumerator noHurtMe()
	{
        if(!HUD.GetComponent<HUDCommands>().HeroHit())
            GameOver();
        collTime = invicTime;
        collHappened = true;
        yield return new WaitForSeconds (invicTime);
		collHappened = false;
		heroSkin.enabled = true;
		curTransTime = 0.0f;
		collTime = 0.0f;
	}

    void FlashSkin()
    {
        if (heroSkin.enabled && curTransTime <= 0.0f)
        {
            heroSkin.enabled = false;
            curTransTime = transTimeMax / collTime;
        }
        else if (curTransTime <= 0.0f)
        {
            heroSkin.enabled = true;
            curTransTime = transTimeMax / collTime;
        }
        curTransTime -= Time.deltaTime;
        collTime -= Time.deltaTime;
    }

    void GameOver()
    {

    }
}
