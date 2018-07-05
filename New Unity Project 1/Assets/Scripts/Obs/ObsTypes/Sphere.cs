﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : Obstacles
{
    private float yStart;
    private float hertz, amp, waveScale;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        yStart = transform.localPosition.y;
        waveScale = transform.localScale.y;
        amp = MAX_HEIGHT;
        hertz = 0.5f * speed;
    }

    protected override void SetHeight(float spawnpoint)
    {
        //To get the total lanes the object can go up and down, we have to do some math. However, this is cube specific, and should be circled back to.
        int temp = Spawner.totalLanes;
        if (temp % 2 != 0)
            temp = (temp - 1) / 2;
        else
            temp = temp / 2;
        MAX_HEIGHT = (int)(-1 * Mathf.Abs(spawnpoint - temp) + temp);
        //end math
    }

    protected override float xMove(float time)
    {
        return -(time * speed) + transform.localPosition.x;
    }

    protected override float yMove(float time)
    {

        return Mathf.Sin(time * hertz) * amp * waveScale + yStart;
    }

    protected override void movement()
    {
        float x, y;
        //calculate movement in x and y. We multiple by lossycale to get accurate speeds
        x = xMove(Time.deltaTime) * gameObject.transform.lossyScale.x;
        y = yMove(Time.time) * gameObject.transform.lossyScale.y;
        Vector2 movVec = new Vector2(x, y);
        Vector2 refVec = Vector2.zero;
        transform.localPosition = Vector2.SmoothDamp(transform.localPosition, movVec, ref refVec, 0.0f, 1000, Time.deltaTime);
    }

    protected override void Update()
    {
        base.Update();
        movement();
    }
}