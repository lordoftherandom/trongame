using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    private GameObject spawner;
    private List<GameObject> allSpawners;
    private readonly string[] objs = { "Circle", "Cube", "Prisim" };
    private int[] spwnWeights = { 5, 1, 1 }; //We want the most circles to spawn
    private const float DEF_SPAWNTM = 80;
    private float spwnrTm, maxSpwnrTm;
    private int diff;
    public GameObject[] spawnPoints;
    

	// Use this for initialization
	void Start () {
        diff = 1;
        allSpawners = new List<GameObject>();
        spawner = Resources.Load("Spawner", typeof(GameObject)) as GameObject;
        if(spawner == null)
            Debug.Log("I guess progress");
        maxSpwnrTm = DEF_SPAWNTM;
        spwnrTm = 0;
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

        int select = Random.Range(0, totalWeight);
        //We reset value of total weight to reuse it in seeing what element should
        //be spawned. If select <= previousWeights+ ObjTypes weight, then we have
        //found the element to spawn.
        totalWeight = 0;
        foreach (ObjType element in System.Enum.GetValues(typeof(ObjType)))
        {
            if (select <= (totalWeight += Objs.getWeight(element)))
            {
                timer = timer/Objs.getWeight(element);

                Objs.changeWeights(element);
                Debug.Log("<color=green>We are adding " + element.ToString()
                    + " spawner</color>");
                GameObject spwnInst = Instantiate(spawner) as GameObject;
                spwnInst.GetComponent<Spawner>().createSpawner(element.ToString(), diff, spawnPoints);
                allSpawners.Add(spwnInst);
                break;
            }
        }
        return timer;
    }//end spawnSpawner
}
