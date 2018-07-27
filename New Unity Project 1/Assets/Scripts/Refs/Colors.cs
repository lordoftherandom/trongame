using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Colors {

    //May not end up using these colors
    private static Color squrcolor = Color.magenta, sphrcolor = Color.magenta, pyrcolor = Color.magenta;
    private static float maxspeed = 7; //starts at 0, but will go higher as the speed increases

    //maybe try anothe rthing instead
    public static Color GetColor(ObjType objs, float speed)
    {
        Color basecolor;

        if (objs == ObjType.Cube)
            basecolor = squrcolor;
        else if (objs == ObjType.Pyrd)
            basecolor = pyrcolor;
        else
            basecolor = sphrcolor;

        return basecolor;
    }

    public static Color MakeColor(float speed)
    {
        //The faster the redder, slower bluer
        float red, blue, currspeed;

        if (speed < 0)
            currspeed = speed * -1;
        else
            currspeed = speed;

        if (maxspeed < currspeed)
            maxspeed = currspeed;

        red = currspeed / maxspeed;

        //To allow for pure blues...
        if (red <= 1/maxspeed)
            red = 0;
        blue = 1 - red;

        return new Color(red, 0, blue, 1);
    }

    public static Color InverseColor(Color color)
    {
        float[] values = new float[3];

        values[0] = color.r - 1;
        values[1] = color.g - 1;
        values[2] = color.b - 1;

        for (int i = 0; i < 3; i++)
        {
            float newVal = Mathf.Abs(values[i]);
            if (newVal > 1)
                newVal -= 1;
            values[i] = newVal;
        }
        color = new Color(values[0], values[1], values[2]);
        return color;
    }

}
