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

	void Start () {
		collHappened = false;
		curTransTime = 0.0f;
        heroSkin = gameObject.GetComponent<Renderer>();
	}
	

	void Update () {
		if (collHappened) {
			if (heroSkin.enabled && curTransTime <= 0.0f) {
				heroSkin.enabled = false;
				curTransTime = transTimeMax/collTime;
			} else if (curTransTime <= 0.0f) {
				heroSkin.enabled = true;
				curTransTime = transTimeMax/collTime;
			}
			curTransTime -= Time.deltaTime;
			collTime -= Time.deltaTime;
		}
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
		Debug.Log("Got to coroutine");
        collTime = invicTime;
        collHappened = true;
        yield return new WaitForSeconds (invicTime);
		collHappened = false;
		heroSkin.enabled = true;
		curTransTime = 0.0f;
		collTime = 0.0f;
	}
}
