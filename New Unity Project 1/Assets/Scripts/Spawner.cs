using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject[] obstacles;
	public float spawnTime;
	public float xSpawn;
	public int totalLanes;
	public List <GameObject> allCurrentObs;
	private int obsCount;
	private float maxSpawnTime;

	// Use this for initialization
	void Start () {
		obsCount = obstacles.Length;
		maxSpawnTime = spawnTime;
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
		if (totalLanes % 2 == 0) // needed. for example, totalLanes = 6. totalLanes/2 = 3. 3 would be a lane that should be choosen, but random.range would only pick up to 3. aadding one solves this.
			laneSelect = Random.Range (-totalLanes/2, totalLanes/2+1);
		else
			laneSelect = Random.Range (-totalLanes/2, totalLanes/2);
		if (laneSelect == -1)
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
