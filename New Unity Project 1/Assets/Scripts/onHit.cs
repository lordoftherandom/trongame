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
    
    /*private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hit " + other.gameObject.name);
        float force = 3.0f;

        if (other.gameObject.tag == "Wall")
        {
            GetComponent<Controller>().mvmntEnabled = false;

            Vector2 dir = other.contacts[0].point - (Vector2) transform.position;
            dir = -dir.normalized;

            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                dir.y = 0;
            else
                dir.x = 0;
            dir = dir.normalized;

            float xDir = dir.x * force, yDir = dir.y * force;

            Vector2 velocity = Vector2.zero;
            Vector2 strtPos = transform.position;
            Vector2 trgPos = new Vector2(xDir, yDir) + strtPos;
            transform.position = Vector2.SmoothDamp(strtPos, trgPos, ref velocity, 0.06f);

            GetComponent<Controller>().mvmntEnabled = true;
        }
    }*/

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

    IEnumerator waitForTime(float time)
    {
        gameObject.GetComponent<Controller>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 100));
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Controller>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, -100));
    }
}
