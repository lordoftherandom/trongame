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

}
