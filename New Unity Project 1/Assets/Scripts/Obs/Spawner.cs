using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private const float DEF_SPWNTM = 6; // because 6 is a good number
    private const int TOTAL_SPAWNS = 25;
    private float minspeed, maxspeed, xSpawn;
    private float spawnTime, diff, maxSpawnTime;
    private GameObject[] spawnPnts;
    private int objstilldeath;
	public static int totalLanes;
	public List <GameObject> allCurrentObs;

	private GameObject obs;


	// Use this for initialization
	void Start () {
        allCurrentObs = new List<GameObject>();
        objstilldeath = TOTAL_SPAWNS;
	}//end Start
	
	// Update is called once per frame
	void Update () {
		if (spawnTime <= 0)
			randomSpawn ();
		spawnTime -= Time.deltaTime;
        //If we have spawned all the objects we can, we need to see
        //if we can remove the spawner
        if (objstilldeath <= 0)
        {
            //If spawners are moving across the screen, don't delete.
            //Spawner is still needed for pauseing functions
            if (allCurrentObs.Count <= 0)
            {
                GetComponentInParent<Map>().RemoveSpawner(this.gameObject);
                Destroy(this.gameObject);
            }
            //Reset the spawntime to max so another object is not spawned
            else
                spawnTime = maxSpawnTime;
        }
	}//end Update

    public void createSpawner(string obsType = "Cube", float strtDif = 1, float smspeed = 1, float bgspeed = 5, 
        GameObject[] strtPos = null)
    {
        obs = Objs.loadType(Objs.toObjType(obsType));
        spawnPnts = strtPos;
        diff = strtDif;
        xSpawn = spawnPnts[0].transform.localPosition.x;
        totalLanes = spawnPnts.Length;
        maxSpawnTime = DEF_SPWNTM / diff;
        minspeed = smspeed;
        maxspeed = bgspeed;
    }

    //Allows us to incremente diff over time, or to change diff
    //if user finds game too hard
    public void chngDiff(float diffChng, bool setValue = false)
    {
        if (setValue)
            diff = diffChng;
        else
            diff = diff + diffChng;
        maxSpawnTime = DEF_SPWNTM / diff;
    }//end chngDiff

    //Called by the DestroyObjs script to remove the collided with ovject
    public void Remove(GameObject obs)
    {
        allCurrentObs.Remove(obs);
        Destroy(obs);
        spawnTime = 0.0f;
    }//end Remove

    public void HideObs()
    {
        int temp = allCurrentObs.Count;
        for (int i = 0; i < temp; i++)
        {
            Renderer reference = allCurrentObs[i].GetComponent<Renderer>();
            Light lightref = allCurrentObs[i].GetComponent<Light>();
            reference.enabled = lightref.enabled = false;
        }
    }//end HideObs

    public void UnHideObs()
    {
        int temp = allCurrentObs.Count;
        for (int i = 0; i < temp; i++)
        {
            Renderer reference = allCurrentObs[i].GetComponent<Renderer>();
            Light lightref = allCurrentObs[i].GetComponent<Light>();
            reference.enabled = lightref.enabled = true;
        }
    }//end UnHideObs

    private void randomSpawn()
	{
        objstilldeath--;
        int spawnPoint = Random.Range(0, spawnPnts.Length);
        GameObject thisInstince = Instantiate(obs, spawnPnts[spawnPoint].transform) as GameObject;
        thisInstince.transform.position = thisInstince.transform.position + new Vector3(0, -0.25f);

	    thisInstince.GetComponent<Obstacles>().setValues(spawnPoint, minspeed, maxspeed, this.gameObject, true);
        thisInstince.GetComponent<Obstacles>().spawner = this.gameObject;
        allCurrentObs.Add(thisInstince);

        //set parent and scale for scaling purposes
        thisInstince.transform.parent = gameObject.transform.parent;
        thisInstince.transform.localScale = new Vector3(1,1,1);

        //we use five here because that is currently the max speed. NO MAGIC NUMBERS
		spawnTime = (maxSpawnTime/5)*(thisInstince.GetComponent<Obstacles>().getSpeed());
	}//end randomSpawn
}
