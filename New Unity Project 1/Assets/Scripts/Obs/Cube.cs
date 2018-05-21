using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Obstacles {
    private float yStart;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        yStart = transform.position.y;
    }
	protected override float xMove(float time)
    {
        return -(time * speed) + transform.position.x;
    }

    protected override float yMove(float time)
    {
        float hertz = 0.5f * speed;
        float wavelength = 0.25f;
        float waveincrease = 5.0f;

        return Mathf.Sin(time * hertz) * wavelength * waveincrease + yStart;
    }

    protected override void movement()
    {
        float x, y;
        x = xMove(Time.deltaTime);
        y = yMove(Time.time);
        Vector2 movVec = new Vector2(x, y);
        Debug.Log("Move " + objID + " at X " + x + " Y " + y, gameObject);
        Vector2 refVec = Vector2.zero;
        transform.position = Vector2.SmoothDamp(transform.position, movVec, ref refVec, 0.0f, 1000, Time.deltaTime);
    }
    // Update is called once per frame
    protected override void Update () {
        base.Update();
        movement();
	}
}
