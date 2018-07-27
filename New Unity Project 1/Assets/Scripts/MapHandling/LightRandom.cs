using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRandom : MonoBehaviour {
    Light[] lights;
    float[] posY;
    float initalY, moveDelay;
    const float MAX_DELAY = 0.05f;
	// Use this for initialization
	void Start () {
        lights = GetComponentsInChildren<Light>();
        initalY = lights[0].transform.localPosition.x;
        posY = new float[] {0.5f, 0.25f, 0, -0.25f, -0.5f};
        moveDelay = MAX_DELAY;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        moveDelay -= Time.deltaTime;
        if(moveDelay < 0)
        {
            moveDelay = MAX_DELAY;
            foreach(Light light in lights)
            {
                int offset = Random.Range(0, posY.Length - 1);
                float x = light.transform.localPosition.x;
                light.transform.localPosition = new Vector3(x, posY[offset]+initalY, 0) ; 
            }
        }

		
	}
}
