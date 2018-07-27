using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTutorial : Obstacles {

	// Use this for initialization
	protected override void Start () {
        speed = 1;
        base.setValues();
	}

    protected override void SetHeight(float spawnpoint)
    {
        ;
    }

    // Update is called once per frame
    protected override void FixedUpdate () {
        base.FixedUpdate();
	}

    protected override void movement()
    {
        ;
    }

    protected override float yMove(float time)
    {
        return 0;
    }

    protected override float xMove(float time)
    {
        return 0;
    }
}
