using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHit : MonoBehaviour {

	public float transTimeMax;
	public float invicTime;
	private bool collHappened;
	private float curTransTime;
	private float collTime;

	void Start () {
		collHappened = false;
		curTransTime = 0.0f;
	}
	

	void Update () {
		if (collHappened) {
			Renderer temp = gameObject.GetComponent<Renderer> ();
			if (temp.enabled && curTransTime <= 0.0f) {
				temp.enabled = false;
				curTransTime = transTimeMax/collTime;
			} else if (curTransTime <= 0.0f) {
				temp.enabled = true;
				curTransTime = transTimeMax/collTime;
			}
			curTransTime -= Time.deltaTime;
			collTime -= Time.deltaTime;
		}
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		print ("Got to Trigger: " + other.gameObject.tag);
		if (other.gameObject.tag == "Obstacle") {
			if (!collHappened) {
				collTime = invicTime;
				collHappened = true;
				StartCoroutine (noHurtMe());
			}
		}
	}

	IEnumerator noHurtMe()
	{
		print ("Got to coroutine");
		yield return new WaitForSeconds (invicTime);
		collHappened = false;
		gameObject.GetComponent<Renderer> ().enabled = true;
		curTransTime = 0.0f;
		collTime = 0.0f;
	}

}
