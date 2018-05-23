using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    private GameObject spawner;
    private List <GameObject> allSpawners;
    private readonly string[] objs = { "Circle", "Cube", "Prisim" };
    private int[] spwnWeights = { 5, 1, 1 }; //We want the most circles to spawn
    private const float DEF_SPAWNTM = 60;
    private float spwnrTm, maxSpwnrTm;
    private int diff;
    

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
	
	// Update is called once per frame
	void Update () {
        spwnrTm -= Time.deltaTime;
        if(spwnrTm <= 0)
        {
            spwnrTm = maxSpwnrTm;
            spawnSpawner();
        }
	}//end Update

    //Chooses a random object (in ObjsType) and creates a spawner of it
    private void spawnSpawner()
    {
        int totalWeight = 0;
        foreach (ObjType element in System.Enum.GetValues(typeof(ObjType)))
        {
            totalWeight += Objs.getWeight(element);
        }

        int select = Random.Range(0, totalWeight);

        foreach (ObjType element in System.Enum.GetValues(typeof(ObjType)))
        {
            if (select <= Objs.getWeight(element))
            {
                Objs.changeWeights(element);
                Debug.Log("<color=green>We are adding " + element.ToString()
                    + " spawner</color>");
                GameObject spwnInst = Instantiate(spawner) as GameObject;
                spwnInst.GetComponent<Spawner>().createSpawner(element.ToString(), diff);
                allSpawners.Add(spwnInst);
                break;
            }
        }

    }//end spawnSpawner
}
