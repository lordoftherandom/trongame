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
    private AudioSource damaged, pickup;
    private HUDCommands HUD;
    [SerializeField]
    private GameObject gameoverscreen;

	void Start () {
		collHappened = false;
		curTransTime = 0.0f;
        heroSkin = gameObject.GetComponent<Renderer>();
        HUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDCommands>();
        AudioSource[] sources = gameObject.GetComponents<AudioSource>();
		if (sources[0].clip.name == "hittune")
		{
			damaged = sources[0];
			pickup = sources[1];
		}
		else
		{
			damaged = sources[1];
			pickup = sources[0];
		}
	}
	

	void Update () {
        if (collHappened)
            FlashSkin();
	}

    void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Obstacle") {
            if (!collHappened)
            {
                other.GetComponent<Obstacles>().destroy();
                StartCoroutine(noHurtMe());
            }
		}
        else if(other.gameObject.tag == "Points")
        {
            if(!collHappened)
            {
                //do effect of powerup, for now, points!
                Obstacles objScript = other.GetComponent<Obstacles>();
                HUD.ScoreIncrease(objScript.scorefactor*5);
                objScript.destroy();
				StartCoroutine(PlaySounds(pickup));
			}
        }
        else if(other.gameObject.tag == "Health")
        {
            if(!collHappened)
            {
                Obstacles objScript = other.GetComponent<Obstacles>();
                int healthLeft = HUD.HPGained();
                if(healthLeft > 0)
                    HUD.ScoreIncrease(objScript.scorefactor * 2 * healthLeft);
                objScript.destroy();
				StartCoroutine(PlaySounds(pickup));
			}
        }
        else if (other.gameObject.tag == "Bomb")
        {
            if (!collHappened)
            {
                Obstacles objScript = other.GetComponent<Obstacles>();
                int bombLeft = HUD.BombGained();
                if(bombLeft > 0)
                    HUD.ScoreIncrease(objScript.scorefactor * 2 * bombLeft);
                objScript.destroy();
				StartCoroutine(PlaySounds(pickup));
			}
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (!collHappened)
            {
                other.GetComponent<Obstacles>().destroy();
                StartCoroutine(noHurtMe());
            }
        }
        else if (other.gameObject.tag == "Points")
        {
            if (!collHappened)
            {
                //do effect of powerup, for now, points!
                Obstacles objScript = other.GetComponent<Obstacles>();
                HUD.ScoreIncrease(objScript.scorefactor * 5);
                objScript.destroy();
				StartCoroutine(PlaySounds(pickup));
            }
        }
        else if (other.gameObject.tag == "Health")
        {
            if (!collHappened)
            {
                Obstacles objScript = other.GetComponent<Obstacles>();
                int healthLeft = HUD.HPGained();
                if (healthLeft > 0)
                    HUD.ScoreIncrease(objScript.scorefactor * 2 * healthLeft);
                objScript.destroy();
				StartCoroutine(PlaySounds(pickup));
			}
        }
        else if (other.gameObject.tag == "Bomb")
        {
            if (!collHappened)
            {
                Obstacles objScript = other.GetComponent<Obstacles>();
                int bombLeft = HUD.BombGained();
                if (bombLeft > 0)
                    HUD.ScoreIncrease(objScript.scorefactor * 2 * bombLeft);
                objScript.destroy();
				StartCoroutine(PlaySounds(pickup));
			}
        }
    }

    IEnumerator noHurtMe()
	{
        if(HUD != null && !HUD.HeroHit())
            GameOver();
        collTime = invicTime;
        collHappened = true;
		StartCoroutine(PlaySounds(damaged));
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
        SoundHandler.PauseAllSounds();
        Instantiate(gameoverscreen);
        Destroy(gameObject);
    }

	IEnumerator PlaySounds(AudioSource source)
	{
		if (source.isPlaying)
			yield return new WaitForSeconds(source.clip.length);
		source.Play();
		SoundHandler.PauseAllSounds();
		yield return new WaitForSeconds(source.clip.length);
		SoundHandler.PauseAllSounds();
	}
}
