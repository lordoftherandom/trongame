using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    #region Members
	public float speed; //The multiplier of speed for the hero
    public bool mvmntEnabled;

    [SerializeField]
    private const float smoothTime = 0.04f;//0.02 per frame
    #endregion

    #region Constructors
    void Start () {
		if(gameObject.GetComponent<Rigidbody2D>()==null)
			gameObject.AddComponent<Rigidbody2D> ();
        mvmntEnabled = true;
	}
	
	
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if(mvmntEnabled)
            moveHero(x, y);
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            Time.timeScale = ++Time.timeScale;
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
        Vector2 strtPos = transform.position;
        Vector2 trgPos = new Vector2(xDir , yDir) + strtPos;
        RaycastHit2D[] hits = new RaycastHit2D[2];

        hits[0] = castARay(new Vector2(xDir, 0) + strtPos);
        hits[1] = castARay(new Vector2(0, yDir) + strtPos);

        if (hits[0].collider != null)
            trgPos.x = strtPos.x;
        if (hits[1].collider != null)
            trgPos.y = strtPos.y;

        transform.position = Vector2.SmoothDamp(strtPos, trgPos, ref velocity, smoothTime);
    }

    void roughMove(float x, float y) { } //blank class, to be filled later

    RaycastHit2D castARay(Vector2 target)
    {
        Vector2 dir = target - (Vector2)this.transform.position;
        dir.Normalize();

        float length;
        if (Mathf.Abs(dir.x) > 0)
            length = 0.45f;
        else
            length = 0.90f;
        Debug.DrawRay((Vector2)transform.position, dir*length, Color.red, 1, false);
        return Physics2D.Raycast((Vector2)transform.position, dir, length);
    }
    
    #endregion
}
