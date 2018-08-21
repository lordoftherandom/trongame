using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    private GameObject spawner, parent;
    private List<GameObject> allSpawners;
    private float spwnrTm, maxSpwnrTm, minspeed, maxspeed;
    private const float incre_minspeed = 0.25f, incre_maxspeed = 0.5f, DEF_SPAWNTM = 50,
        decayLimit = 10.0f, decay = 0.2f, MAX_SPAWNTM = 60;
    private int powerupRate = 50; //repersented by the inverse of rate
    private int diff;
    public GameObject[] spawnPoints;
    

	// Use this for initialization
	void Start () {
        diff = 1; //def value, once diff is included needs to change
        allSpawners = new List<GameObject>();

        spawner = Resources.Load("Spawner", typeof(GameObject)) as GameObject; //Load spawner object
        parent = GameObject.FindGameObjectWithTag("Parent"); //Find parent to attach spawners to

        maxSpwnrTm = DEF_SPAWNTM;
        spwnrTm = 0;
        minspeed = 1;
        maxspeed = 5;
    }
	
	// Every frame we count spwnrTm down. Once 0, we spawn a spawner
	void FixedUpdate () {
        spwnrTm -= Time.deltaTime;
        if(spwnrTm <= 0)
        {
            spwnrTm = spawnSpawner(maxSpwnrTm);
            Debug.Log("<color=purple>Spawner Time set to " + spwnrTm + "</color>");
        }
        if (allSpawners.Count < 1)
            spwnrTm = spawnSpawner(maxSpwnrTm);
	}//end Update

    //Chooses a random object (in ObjsType) and creates a spawner of it.
    //Returns an adjusted spawn timer according to the weight of the spawned
    //Object, and the passed in value
    private float spawnSpawner(float timer)
    {
        if (maxSpwnrTm > decayLimit)
            maxSpwnrTm -= decay;
        int totalWeight = 0;
        //First, get the total weight of all objects
        foreach (ObjType element in System.Enum.GetValues(typeof(ObjType)))
        {
            totalWeight += Objs.getWeight(element);
        }

        int select = Random.Range(0, totalWeight);//This is our object!
      
        int wghtSoFar = 0;
        foreach (ObjType element in System.Enum.GetValues(typeof(ObjType)))
        {
            //When total weigth <= totalweight + possible current obs weight, we have found our obs type to spawn
            if (select <= (wghtSoFar += Objs.getWeight(element)))
            {

                timer = timer/((Objs.GetTotalObjs() - (int)element +1)); 
                Objs.changeWeights(element);

                Debug.Log("<color=green>We are adding " + element.ToString()
                    + " spawner</color>");

                GameObject spwnInst = Instantiate(spawner) as GameObject;

                spwnInst.GetComponent<Spawner>().createSpawner(element.ToString(), diff, minspeed, maxspeed, 
                    powerupRate, spawnPoints);

                minspeed += incre_minspeed;
                maxspeed += incre_maxspeed;
                //Set spawner's parent to the parent gameobject for scaleing reasons
                spwnInst.transform.parent = parent.transform;

                allSpawners.Add(spwnInst);
                break;
            }
        }
		if (timer > MAX_SPAWNTM)
			timer = MAX_SPAWNTM;
		else if (timer < decayLimit)
			timer = decayLimit;
        return timer;
    }//end spawnSpawner

    //Invoked by a spawner that is about to destroy itself
    public void RemoveSpawner(GameObject spawner)
    {
        if (!allSpawners.Remove(spawner))
            Debug.Log("Not an actual spawner");
    }

    //Used to invoke spawners hide/unhideobs functions. Should only be used on pause
    public void HideObs()
    {
        foreach(GameObject spawner in allSpawners)
            spawner.GetComponent<Spawner>().HideObs();
    }
    public void UnHideObs()
    {
        foreach (GameObject spawner in allSpawners)
            spawner.GetComponent<Spawner>().UnHideObs();
    }
}
