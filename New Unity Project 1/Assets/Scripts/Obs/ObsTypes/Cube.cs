﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Obstacles
{

    protected override void SetHeight(float spawnpoint)
    {
        MAX_HEIGHT = 1;
    }

    protected override void Update()
    {
        base.Update();
        movement();
    }

    protected override float xMove(float time)
    {
        return -(time * speed) + transform.localPosition.x;
    }

    protected override float yMove(float time)
    {
        //Cubes do not move in y direction
        return 0;
    }

    protected override void movement()
    {
        float x, y;
        //calculate movement in x and y. We multiple by lossycale to get accurate speeds
        x = xMove(Time.deltaTime) * gameObject.transform.lossyScale.x;
        Vector2 movVec = new Vector2(x, gameObject.transform.localPosition.y);
        Vector2 refVec = Vector2.zero;
        transform.localPosition = Vector2.SmoothDamp(transform.localPosition, movVec, ref refVec, 0.0f, 1000, Time.deltaTime);
    }
}
