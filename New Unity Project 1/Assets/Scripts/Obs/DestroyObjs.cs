using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjs : MonoBehaviour {
	void Start () { }
	void Update () { }

    //Where points should be added
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            other.gameObject.GetComponent<Obstacles>().spawner
                .GetComponent<Spawner>().Remove(other.gameObject);
        }
    }
}
