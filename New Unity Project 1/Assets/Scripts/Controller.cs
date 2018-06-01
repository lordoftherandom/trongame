using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    #region Members
    private Rigidbody2D hero; //To move hero around screen

	public float speed; //The multiplier of speed for the hero

    [SerializeField]
    private const float smoothTime = 0.04f;//0.02 per frame
    #endregion

    #region Constructors
    void Start () {
		if((hero = gameObject.GetComponent<Rigidbody2D>())==null)
        { 
			gameObject.AddComponent<Rigidbody2D> ();
			hero = gameObject.GetComponent<Rigidbody2D>();
		}
	}
	
	
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveHero(x, y);
	}
    #endregion

    #region Functions
    //Handles movement of the hero. allows us to pick between smoothMovement and roughMovement
    //Should be phased into one or ther other before final release
    void moveHero(float x, float y, bool smooth = true)
    {
        if (smooth)
            smoothMove(x, y);
        else
            roughMove(x, y);

	}

    //Applies speed to x && y inputs, creates des. vector, and smoothly moves the hero there
    void smoothMove(float x, float y)
    {
        float xDir = x * speed, yDir = y * speed;

        Vector2 velocity = Vector2.zero;
        Vector2 strtPos = hero.transform.position;
        Vector2 trgPos = new Vector2(xDir , yDir) + strtPos;
        
        hero.transform.position = Vector2.SmoothDamp(strtPos, trgPos, ref velocity, smoothTime);
    }

    void roughMove(float x, float y) { } //blank class, to be filled later
    #endregion
}
