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
	private bool delayX;
	private bool delayY;
	private float horMovement;
	private float verMovement;
	private ScaleToScreen scaleMultiple;

	// Use this for initialization
	void Start () {
		hero = gameObject.GetComponent<Rigidbody2D>();
		if (hero == null) {
			gameObject.AddComponent<Rigidbody2D> ();
			hero = gameObject.GetComponent<Rigidbody2D>();
		} 
		scaleMultiple = Camera.main.GetComponent<ScaleToScreen>();
		delayX = false;
		delayY = false;
		horMovement = 1.0f;
		verMovement = 2.0f;

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
		if (!delayX) {
			if (xDir > 0.0f) {
				hero.MovePosition (new Vector2 (horMovement*scaleMultiple.GetScaleMultiple(), 0) + (new Vector2 (transform.position.x, transform.position.y)));
				tempDir = 1;
			} else if (xDir < 0.0f) {
				hero.MovePosition (new Vector2 (-horMovement*scaleMultiple.GetScaleMultiple(), 0) + (new Vector2 (transform.position.x, transform.position.y)));
				tempDir = -1;
			}
			StartCoroutine (delayCountX (tempDir));
		}
		if (!delayY) {
			if (yDir > 0.0f) {
				hero.MovePosition ((new Vector2 (0, verMovement*scaleMultiple.GetScaleMultiple())) + (new Vector2 (transform.position.x, transform.position.y)));
				tempDir = 1;
			} else if (yDir < 0.0f) {
				hero.MovePosition ((new Vector2 (0, -verMovement*scaleMultiple.GetScaleMultiple())) + (new Vector2 (transform.position.x, transform.position.y)));
				tempDir = -1;
			}
			StartCoroutine (delayCountY (tempDir));
		}

		//jumpCheck (yDir);

	}
		

	IEnumerator delayCountY(int dir)
	{
		delayY = true;
		yield return new WaitForSeconds (delayMax);
		delayY = false;
	}

	IEnumerator delayCountX(int dir)
	{
		delayX = true;
		yield return new WaitForSeconds (delayMax);
		delayX = false;
	}
}
