using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject[] obstacles;
	public float spawnTime;
	public float xSpawn;
	public int totalLanes;
	public List <GameObject> allCurrentObs;
	public float scale;

	private int obsCount;
	private float maxSpawnTime;
	private int laneMax;
	private int laneMin;


	// Use this for initialization
	void Start () {
		if (totalLanes / 2 % 2 != 0) //adding .5 makes sure the number will converte to an int in the case that total lanes are odd
			laneMax = (int)((totalLanes / 2)+0.5);
		else
			laneMax = (int)((totalLanes / 2)+1); //adding 1 makes sure that, when laneMax is put into the random number generator, it will spawn at the max lane
		laneMin = totalLanes / 2 * -1;

		obsCount = obstacles.Length;
		maxSpawnTime = spawnTime;
		print ("laneMax: " + laneMax);
		print ("laneMin: " + laneMin);
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTime <= 0)
			randomSpawn ();
		spawnTime -= Time.deltaTime;
		
	}

	void randomSpawn()
	{
		int laneSelect;
		int mutipleLanes = 1;
		laneSelect = Random.Range (laneMin, laneMax);
		if (laneSelect == -1) //is there a way to make this dynamic?
			mutipleLanes = Random.Range (-1, 2)*6;
		else if (laneSelect == 0)
			mutipleLanes = Random.Range (-2, 3)*6;
		else if (laneSelect == 1)
			mutipleLanes = Random.Range (-1, 2)*6;
		if (mutipleLanes == 0)
			mutipleLanes = 1;
		GameObject ranObject = obstacles[(Random.Range(0,obsCount))];
		GameObject thisInstince = Instantiate (ranObject, new Vector3 (xSpawn, laneSelect*2, 0 ), Quaternion.identity);
		allCurrentObs.Add (thisInstince);
		thisInstince.GetComponent<obsMovement> ().waveIncrease = mutipleLanes;
		spawnTime = maxSpawnTime;
	}

	public void Remove(GameObject obs)
	{
		allCurrentObs.Remove(obs);
		spawnTime = 0.0f;
	}

	public void HideObs()
	{
		int temp = allCurrentObs.Count;
		for (int i = 0; i < temp; i++) {
			Renderer reference = allCurrentObs [i].GetComponent<Renderer>();
			reference.enabled = false;
		}
	}

	public void UnHideObs()
	{
		int temp = allCurrentObs.Count;
		for (int i = 0; i < temp; i++) {
			Renderer reference = allCurrentObs [i].GetComponent<Renderer>();
			reference.enabled = true;
		}
	}

}
