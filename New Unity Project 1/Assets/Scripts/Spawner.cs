using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public float spawnTime;
	public float xSpawn;
	public int totalLanes;
	public List <GameObject> allCurrentObs;
	private float maxSpawnTime;
	private GameObject parent;


	// Use this for initialization
	void Start () {

		maxSpawnTime = spawnTime;
		parent = GameObject.FindGameObjectWithTag ("Parent");

	}//end Start
	
	// Update is called once per frame
	void Update () {
		if (spawnTime <= 0)
			randomSpawn ();
		spawnTime -= Time.deltaTime;
		
	}//end Update

	void randomSpawn()
	{


		GameObject obs = Resources.Load ("Cube", typeof(GameObject)) as GameObject;
		GameObject thisInstince = Instantiate (obs, new Vector3 (xSpawn, totalLanes, 0),
			Quaternion.identity) as GameObject;

		thisInstince.AddComponent<Cube> ();
		thisInstince.GetComponent<Cube> ().setValues (totalLanes, 1, 5, this.gameObject, true);


		spawnTime = maxSpawnTime;
	}//end randomSpawn

	public void Remove(GameObject obs)
	{
		allCurrentObs.Remove(obs);
		spawnTime = 0.0f;
	}//end Remove

	public void HideObs()
	{
		int temp = allCurrentObs.Count;
		for (int i = 0; i < temp; i++) {
			Renderer reference = allCurrentObs [i].GetComponent<Renderer>();
			reference.enabled = false;
		}
	}//end HideObs

	public void UnHideObs()
	{
		int temp = allCurrentObs.Count;
		for (int i = 0; i < temp; i++) {
			Renderer reference = allCurrentObs [i].GetComponent<Renderer>();
			reference.enabled = true;
		}
	}//end UnHideObs

}
