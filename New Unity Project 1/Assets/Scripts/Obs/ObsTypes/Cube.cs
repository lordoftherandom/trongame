using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Obstacles
{
    protected override void Start()
    {
        base.Start();
        scorefactor = 1f;
        objType = ObjType.Cube;
    }

    protected override void SetHeight(float spawnpoint)
    {
        MAX_HEIGHT = 1;
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
        //calculate movement in x and y. We multiple by lossycale to get accurate speeds
        float x = xMove(Time.deltaTime) * gameObject.transform.lossyScale.x;
        Vector2 movVec = new Vector2(x, gameObject.transform.localPosition.y);
        Vector2 refVec = Vector2.zero;
        transform.localPosition = Vector2.SmoothDamp(transform.localPosition, movVec, ref refVec, 0.0f, 1000, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("It detects this");
        if (collision.gameObject.tag == "RightWall")
        {
            Debug.Log("So does it detect this?");
            SoundHandler.QueueSound(objType);
        }
    }
}
