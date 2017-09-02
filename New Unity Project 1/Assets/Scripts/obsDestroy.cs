using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsDestroy : MonoBehaviour {
	private float xRange; //Distance until object should be destroyed
	private GameObject spawnerHolder;
	// Use this for initialization
	void Start () {
		xRange = 15.0f;
		spawnerHolder = GameObject.FindGameObjectWithTag ("Spawner");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -xRange) { //xRange is negative to make it easier to understand when editing
			spawnerHolder.GetComponent<Spawner>().Remove(this.gameObject);
			Destroy (this.gameObject);
		}
	}
}
