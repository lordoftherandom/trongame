using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : Obstacles
{
    private float radius, xoncircle, yoncirlce, radiussqrt, 
        currx, ystart;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        scorefactor = 2.0f;
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
                Debug.Log("Coming into low. Adjusting");
                gameObject.transform.localPosition = 
                    new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 0.5f);
            }
            else
            {
                Debug.Log("Coming into high. Adjusting");
                gameObject.transform.localPosition = 
                    new Vector2( gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 0.5f);
            }
        }
        else
            radius = Random.Range(0.25f, 2.0f-Mathf.Abs(spawnpoint-2.0f));

        radiussqrt = Mathf.Sqrt(radius);
        xoncircle = radiussqrt;
        yoncirlce = 0.0f;
        currx = gameObject.transform.localPosition.x;
        ystart = gameObject.transform.localPosition.y;
    }

    protected override float xMove(float time)
    {
        currx -= time;
        xoncircle = (xoncircle + speed*time) % (radiussqrt*4);

        if (xoncircle <= radiussqrt * 2)
        {
            float newxcircle;
            newxcircle = xoncircle - radiussqrt;
            newxcircle *= newxcircle;

            newxcircle = radius - newxcircle;
            return Mathf.Sqrt(newxcircle);
        }
        else
        {
            float newxcircle;
            newxcircle = xoncircle - radiussqrt * 3;
            newxcircle *= newxcircle;

            newxcircle = radius - newxcircle;
            return Mathf.Sqrt(newxcircle) * -1;
        }
    }

    protected override float yMove(float time)
    {
        yoncirlce = (yoncirlce + speed*time) % (radiussqrt * 4);

        if(yoncirlce <= radiussqrt*2)
        {
            float newycircle;
            newycircle = yoncirlce - radiussqrt;
            newycircle *= newycircle;

            newycircle = radius - newycircle;
            return Mathf.Sqrt(newycircle);
        }
        else
        {
            float newycircle;
            newycircle = yoncirlce - radiussqrt * 3;
            newycircle *= newycircle;

            newycircle = radius - newycircle;
            return Mathf.Sqrt(newycircle)*-1;
        }
    }

    protected override void movement()
    {
        float x, y;
        //calculate movement in x and y. We multiple by lossycale to get accurate speeds
        x = (xMove(Time.deltaTime) + currx*speed) * gameObject.transform.lossyScale.x;
        y = (yMove(Time.deltaTime) + ystart) * gameObject.transform.lossyScale.y;
        Vector2 movVec = new Vector2(x, y);
        Vector2 refVec = Vector2.zero;
        transform.localPosition = movVec;
    }

}
