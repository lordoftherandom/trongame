using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsDestroy : MonoBehaviour {
	private float xRange; //Distance until object should be destroyed
	private GameObject border;
	// Use this for initialization
	void Start () {
		border = GameObject.FindGameObjectWithTag ("DestroyPoint");

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < border.transform.position.x) { //xRange is negative to make it easier to understand when editing
			this.gameObject.GetComponent<Obstacles>().destroy();
		}
	}
}
