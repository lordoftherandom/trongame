using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsMovement : MonoBehaviour {
	public float hertz; //will speed up how fast obs bounces
	public bool ranBounce; //will add variance to the speed of the bounce
	public float speed; //will make go horizontally faster
	public bool rotates; //toggle and object will rotate
	public bool ranSpeed; //toggle random speed
	public int minSpeed; //the minimum speed an object can go
	public int maxSpeed;  //if ranspeed is randomized, this is the max mutiple of the speed
	public bool ranRotate; //will add slight variance to rotation based on object speed
	public float xRotate; //speed object will rotate along x-axis, turn to private on final build
	public float yRotate; //speed object will rotate along y-axis, turn to private on final build
	public float zRotate; //speed object will rotate along z-axis, turn to private on final build
	public float rotateDamp; //the dampener on rotation, turn to private on final build
	public float waveIncrease;

	private float scaleMultiple;
	private float wavelength; // if cube spawns in the middle of the lane, this is how much room the cube has to bounce/2
	private float smoothTime; //how fast, roughly, it takes from frame to frame. Smooths bounces
	private float offsetY; //needed to give orignal point of object
	private float forwardX; //keeps track of how far object should go
	private Vector2 refVelocity; //the reference velocity set for the damping function. Should not be zero.

	// Use this for initialization
	void Start () {
		smoothTime = 0.0f; //number found after testing
		offsetY = transform.position.y;
		refVelocity = Vector2.zero;
		wavelength = 0.25f;
		scaleMultiple = Camera.main.gameObject.GetComponent<ScaleToScreen> ().scaleMultiple;

		if (ranSpeed) { //will randomize speed of cubes, and possibly randomize rotation
			int temp = Random.Range (minSpeed, maxSpeed);
			speed = speed * temp*scaleMultiple;
			if (ranRotate) { //randomizes rotation of cubes, based on speed and randomly selects if it will rotate clockwise or counterclockwise
				if (Random.Range (0, 2) == 0) //using random range for each of these seems ineffiective, but increases the variance in how objects spin. Allows for 8 total combinations
					xRotate = xRotate * temp / rotateDamp;
				else
					xRotate = xRotate * temp / rotateDamp * -1;
				if (Random.Range (0, 2) == 0)
					yRotate = yRotate * temp / rotateDamp;
				else
					yRotate = yRotate * temp / rotateDamp * -1;
				if (Random.Range (0, 2) == 0)
					zRotate = zRotate * temp / rotateDamp;
				else
					zRotate = zRotate * temp / rotateDamp * -1;
			}
			if (ranBounce) {
				if (Random.Range (0, 2) == 0)
					hertz = hertz * temp/scaleMultiple;
				else
					hertz = hertz * temp * -1/scaleMultiple;
			}
		}



	}
	
	// Update is called once per frame
	void Update () {
		float temp = Time.deltaTime;
		forwardX = -Time.deltaTime*speed;
		float temp1 = Mathf.Sin (Time.time*hertz)*wavelength*waveIncrease;
		Vector2 temp2 = new Vector2 (transform.position.x + forwardX, offsetY + temp1);
		transform.position = Vector2.SmoothDamp (transform.position, temp2, ref refVelocity, smoothTime, 1000, Time.deltaTime);
		if (rotates)
			transform.Rotate (xRotate * temp, yRotate * temp, zRotate * temp);
	}
		
}
