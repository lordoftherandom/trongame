using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private const float DEF_SPWNTM = 6; // because 8 is a good number
    private float spawnTime, diff, maxSpawnTime;
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
        int lanes = 1, float xStart = 15)
    {
        obs = Objs.loadType(Objs.toObjType(obsType));

        diff = strtDif;
        xSpawn = xStart;
        totalLanes = lanes;
        maxSpawnTime = DEF_SPWNTM / diff;
    }//end createSpawnner

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
		GameObject thisInstince = Instantiate (obs, new Vector3 (xSpawn, totalLanes, 0),
			Quaternion.identity) as GameObject;
		thisInstince.GetComponent<Obstacles> ().
            setValues(totalLanes, 1, 5, this.gameObject, true);
        allCurrentObs.Add(thisInstince);
		spawnTime = maxSpawnTime;
	}//end randomSpawn
}
