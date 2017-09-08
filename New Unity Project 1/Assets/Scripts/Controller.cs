using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	private float x; //To capture horizontal movement
	private float y;
	private Rigidbody2D hero; //To move hero around screen
	public float speed; //The multiplier of speed for the hero
	public float jumpForce; //How much force is applied to the hero on jump
	private Vector2 movement;
	public float delayMax;
	//private float delay;
	private float delayX;
	private float delayY;

	// Use this for initialization
	void Start () {
		hero = gameObject.GetComponent<Rigidbody2D>();
		if (hero == null) {
			gameObject.AddComponent<Rigidbody2D> ();
			hero = gameObject.GetComponent<Rigidbody2D>();
		} 
		delayX = 0;
		delayY = 0;
	}
	
	// Update is called once per frame
	void Update () {
		x = Input.GetAxisRaw ("Horizontal");
		y = Input.GetAxisRaw ("Vertical");
		moveHero (x,y);

	}

	void FixedUpdate(){
	}
		
	void moveHero(float xDir, float yDir)
	{
		int tempDir = 0;
		if (delayX == 0) {
			if (xDir > 0) {
				hero.MovePosition (new Vector2 (1, 0) + (new Vector2 (transform.position.x, transform.position.y)));
				tempDir = 1;
			} else if (xDir < 0) {
				hero.MovePosition (new Vector2 (-1, 0) + (new Vector2 (transform.position.x, transform.position.y)));
				tempDir = -1;
			}
			StartCoroutine (delayCountX (tempDir));
		}
		if (delayY == 0) {
			if (yDir > 0) {
				hero.MovePosition ((new Vector2 (0, 2.0f)) + (new Vector2 (transform.position.x, transform.position.y)));
				tempDir = 1;
			} else if (yDir < 0) {
				hero.MovePosition ((new Vector2 (0, -2.0f)) + (new Vector2 (transform.position.x, transform.position.y)));
				tempDir = -1;
			}
			StartCoroutine (delayCountY (tempDir));
		}

		//jumpCheck (yDir);

	}
		

	IEnumerator delayCountY(int dir)
	{
		delayY = 1;
		yield return new WaitForSeconds (delayMax);
		delayY = 0;
	}

	IEnumerator delayCountX(int dir)
	{
		delayX = 1;
		yield return new WaitForSeconds (delayMax);
		delayX = 0;
	}
}
