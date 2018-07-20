using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    #region Attributes
    private const float DEF_SPWNTM = 7; // because 8 is a good number
    private const int TOTAL_SPAWNS = 30;

    private float minspeed, maxspeed;
    private float spawnTime, diff, maxSpawnTime;

    private GameObject obs;
    private GameObject[] spawnPnts;

    public int objstilldeath, powerupChanceCurr;

    public int powerupChance;
	public static int totalLanes;

	public List <GameObject> allCurrentObs;
    private ObjType objType;
    #endregion

    #region Constructors
    void Start () {
        allCurrentObs = new List<GameObject>();
        objstilldeath = TOTAL_SPAWNS;
	}//end Start
	
	// Update is called once per frame
	void FixedUpdate () {
		if (spawnTime <= 0 && objstilldeath > 0)
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
        int pwrChance = 25, GameObject[] strtPos = null)
    {
        objType = Objs.toObjType(obsType);
        obs = Objs.loadType(objType);
        spawnPnts = strtPos;
        diff = strtDif;
        totalLanes = spawnPnts.Length;
        maxSpawnTime = DEF_SPWNTM / diff;
        minspeed = smspeed;
        maxspeed = bgspeed;
        powerupChance = powerupChanceCurr = pwrChance;
    }
    #endregion

    #region Methods
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

    //Called by the DestroyObjs script to remove the collided with object
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
        string objName;

        GameObject thisInstince = Instantiate(obs, spawnPnts[spawnPoint].transform) as GameObject;
        Obstacles objScript = thisInstince.GetComponent<Obstacles>();
        
        //set parent and scale for scaling purposes
        thisInstince.transform.parent = gameObject.transform.parent;
        thisInstince.transform.localScale = new Vector3(1, 1, 1);

        thisInstince.transform.position += new Vector3(0, -0.25f);

        if (PowerupSpawn())
        {
            int powerUpType = Random.Range(0, 3);

            if (powerUpType == 2)
                thisInstince.tag = "Points";
            else if (powerUpType == 1)
                thisInstince.tag = "Bomb";
            else
                thisInstince.tag = "Health";

            objScript.isPowerup = true;
            objName = thisInstince.tag;
        }
        else
            objName = objType.ToString();

	    objScript.setValues(spawnPoint, minspeed, maxspeed, this.gameObject, true);
        objScript.spawner = this;
        allCurrentObs.Add(thisInstince);

        thisInstince.name = objName + objScript.objID;


        spawnTime = (maxSpawnTime/maxspeed)*(objScript.getSpeed());
	}//end randomSpawn

    private bool PowerupSpawn()
    {
        if (Random.Range(0, powerupChance + 1) >= powerupChanceCurr--)
        {
            powerupChanceCurr = powerupChance;
            return true;
        }
        else
            return false;
    }
    #endregion
}
