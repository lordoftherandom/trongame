using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjType
{
    Cube, Sphr, Pyrd,
}//end ObjType

public static class Objs {

    private static int[] weights = { 5, 1, 1 };

    public static ObjType toObjType(string obj)
    {
        switch(obj)
        {
            case "Cube":
                return ObjType.Cube;
            case "Sphr":
                return ObjType.Sphr;
            case "Pyrd":
                return ObjType.Pyrd;
            default:
                Debug.Log("<color=yellow>toObjType failed. Returning Cube instead</color>");
                return ObjType.Cube;
        }
    }//end toObjType

    public static int getWeight(ObjType obj)
    {
        return weights[(int)obj];
    }//end getWeight

    //Should be called when a spawner of any type is created
    //First adds 1 to the lowest valued type, and subtracts
    //one from the lowest value
    public static void changeWeights(ObjType obj)
    {
        int minPos = 0;
        for(int i = 1; i < weights.Length; i++)
        {
            if (weights[minPos] > weights[i])
                minPos = i;
        }
        weights[minPos]++;
        weights[(int)obj]--;
    }//end changeWeights

    

    public static GameObject loadType(ObjType obj)
    {
        GameObject obs;
        if ((obs = Resources.Load(obj.ToString(), typeof(GameObject)) as GameObject) == null)
        {//If the resource, for some reason, could not be loaded, load a cube instead
            Debug.Log("<color=yellow>Object could not be loaded</color>");
            obs = Resources.Load("Cube", typeof(GameObject)) as GameObject;
        }
        return obs;
    }//end loadType
}
