using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    private GameObject spawner, parent;
    private List<GameObject> allSpawners;
    //private readonly string[] objs = { "Sphr", "Cube", "Pyrd" };
    private const float DEF_SPAWNTM = 80;
    private float spwnrTm, maxSpwnrTm, minspeed, maxspeed;
    private const float incre_minspeed = 0.25f, incre_maxspeed = 0.5f;
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
	void Update () {
        spwnrTm -= Time.deltaTime;
        if(spwnrTm <= 0)
        {
            spwnrTm = spawnSpawner(maxSpwnrTm);
            Debug.Log("<color=purple>Spawner Time set to " + spwnrTm + "</color>");
        }
	}//end Update

    //Chooses a random object (in ObjsType) and creates a spawner of it.
    //Returns an adjusted spawn timer according to the weight of the spawned
    //Object, and the passed in value
    private float spawnSpawner(float timer)
    {
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
                //Set timer to be a function of maxSpawnTime/how likely obj was to spawn.
                float divisor;
                if((divisor = totalWeight - Objs.getWeight(element)) <= 0)
                {
                    divisor = 1;
                }
                timer = timer/divisor; 
                Objs.changeWeights(element);

                Debug.Log("<color=green>We are adding " + element.ToString()
                    + " spawner</color>");

                GameObject spwnInst = Instantiate(spawner) as GameObject;
                spwnInst.GetComponent<Spawner>().createSpawner(element.ToString(), diff, minspeed, maxspeed, spawnPoints);
                minspeed += incre_minspeed;
                maxspeed += incre_maxspeed;
                //Set spawner's parent to the parent gameobject for scaleing reasons
                spwnInst.transform.parent = parent.transform;

                allSpawners.Add(spwnInst);
                break;
            }
        }
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
