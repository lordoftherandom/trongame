using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : Obstacles
{
    private static AudioClip sound;
    private float radius, ystart, xCurr,slowDown = 1.25f;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        scorefactor = 2.0f;
        objType = ObjType.Sphr;
    }
    
    //Because of circle movements, we must ensure spot has 2 lanes of space minimum
    protected override void SetHeight(float spawnpoint)
    {
        MAX_HEIGHT = spawnpoint;

        //values are hardcoded; should be changed
        if (spawnpoint < 1 || spawnpoint > 3)
        {
            radius = 0.25f;
            if (spawnpoint == 0)
            {
                gameObject.transform.localPosition = 
                    new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 0.5f);
            }
            else
            {
                gameObject.transform.localPosition = 
                    new Vector2( gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 0.5f);
            }
        }
        else
            radius = Random.Range(0.25f, 2.0f-Mathf.Abs(spawnpoint-2.0f));

        radius *= 2;
        xCurr = gameObject.transform.localPosition.x;
        ystart = gameObject.transform.localPosition.y;
    }

    protected override float xMove(float time)
    {
        return  Mathf.Cos(time) * radius;
    }

    protected override float yMove(float time)
    {
        return  Mathf.Sin(time) * radius;
    }

    protected override void movement()
    {
        float x, y, time = Time.time*speed;
        xCurr -= speed * Time.deltaTime / slowDown;
        //calculate movement in x and y. We multiple by lossycale to get accurate speeds
        x = (xMove(time) + xCurr) * gameObject.transform.lossyScale.x;
        y = (yMove(time) + ystart) * gameObject.transform.lossyScale.y;
        Vector2 movVec = new Vector2(x, y);
        Vector2 refVec = Vector2.zero;
        transform.localPosition = movVec;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RightWall")
        {
            SoundHandler.QueueSound(objType, (int)Mathf.Ceil(speed));
        }
    }

}
