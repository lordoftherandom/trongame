using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private const float DEF_SPWNTM = 6; // because 8 is a good number
    private float spawnTime, diff, maxSpawnTime;
    private GameObject[] spawnPnts;
	public float xSpawn;
	public int totalLanes;
	public List <GameObject> allCurrentObs;

	private GameObject obs;


	// Use this for initialization
	void Start () {
        allCurrentObs = new List<GameObject>();
	}//end Start
	
	// Update is called once per frame
	void Update () {
		if (spawnTime <= 0)
			randomSpawn ();
		spawnTime -= Time.deltaTime;
	}//end Update

    //Allows us to create new spawnners with w/e starting conditions we want. xStart and lanes
    //might be worked out in future releases
     public void createSpawner(string obsType = "Cube", float strtDif = 1, 
        int lanes = 1)
    {
        obs = Objs.loadType(Objs.toObjType(obsType));

        diff = strtDif;
        xSpawn = spawnPnts[0].transform.localPosition.x;
        totalLanes = lanes;
        maxSpawnTime = DEF_SPWNTM / diff;
    }//end createSpawnner

    public void createSpawner(string obsType = "Cube", float strtDif = 1, GameObject[] strtPos = null)
    {
        obs = Objs.loadType(Objs.toObjType(obsType));
        spawnPnts = strtPos;
        diff = strtDif;
        xSpawn = spawnPnts[0].transform.localPosition.x;
        totalLanes = spawnPnts.Length;
        maxSpawnTime = DEF_SPWNTM / diff;
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

    public void Remove(GameObject obs)
    {
        allCurrentObs.Remove(obs);
        spawnTime = 0.0f;
    }//end Remove

    public void HideObs()
    {
        int temp = allCurrentObs.Count;
        for (int i = 0; i < temp; i++)
        {
            Renderer reference = allCurrentObs[i].GetComponent<Renderer>();
            reference.enabled = false;
        }
    }//end HideObs

    public void UnHideObs()
    {
        int temp = allCurrentObs.Count;
        for (int i = 0; i < temp; i++)
        {
            Renderer reference = allCurrentObs[i].GetComponent<Renderer>();
            reference.enabled = true;
        }
    }//end UnHideObs

    private void randomSpawn()
	{

        int spawnPoint = Random.Range(0, spawnPnts.Length);
        GameObject thisInstince = Instantiate(obs, spawnPnts[spawnPoint].transform) as GameObject;
        //To get the total lanes the object can go up and down, we have to do some math. However, this is cube specific, and should be circled back to.
        int temp = spawnPnts.Length;
        if (temp % 2 != 0)
            temp = (temp - 1) / 2;
        else
            temp = temp / 2;
        int totalLanes = -1 * Mathf.Abs(spawnPoint - temp) + temp;
        //end math

        Debug.Log("<color=green> Spawning " + obs.name + " with totalLanes " + totalLanes + "</color>");

	    thisInstince.GetComponent<Obstacles> ().setValues(totalLanes, 1, 5, this.gameObject, true);
        allCurrentObs.Add(thisInstince);

        //set parent and scale for scaling purposes
        thisInstince.transform.parent = gameObject.transform.parent;
        thisInstince.transform.localScale = new Vector3(1,1,1);
        thisInstince.GetComponentInChildren<Renderer>().material.color = 
            Colors.MakeColor(thisInstince.GetComponent<Obstacles>().speed);
        Debug.Log("Color is " + thisInstince.GetComponentInChildren<Renderer>().material.color);
        //we use five here because that is currently the max speed. NO MAGIC NUMBERS
		spawnTime = (maxSpawnTime/5)*(thisInstince.GetComponent<Obstacles>().getSpeed());
	}//end randomSpawn
}
