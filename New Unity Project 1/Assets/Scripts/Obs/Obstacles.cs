﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Parent Class for all other Obstcales
//Sets speed and rotation, and handles the rotation of object
//Movement should be handled inside the children class
//Created: 05-12-18
//Modifed: 05-19-18
public abstract class Obstacles : MonoBehaviour {
    static int count = 0; //debugging purpose only
    protected int objID;
	private bool rotate;
	private Vector3 rotateVec;
	protected float MAX_HEIGHT;
	protected float speed;
	protected GameObject parent;

	protected virtual void Start () {
        objID = count++;
	}//end start

	
	// Update is called once per frame
	protected virtual void Update () {
		float time = Time.deltaTime;
		if (rotate)
			transform.Rotate (rotateVec * time);
	}//end Update

	public void destroy()
	{
		parent.GetComponent<Spawner>().Remove (this.gameObject);
		Destroy (this.gameObject);
	
	}//end destroy

	public void chgnSpeed(float x)
	{
		speed = speed * x;
	}//end chgnSpeed

	GameObject getParent()
	{
		return parent;
	}//end getParent

    public void setValues(float height, int minSp, int maxSp, GameObject creator, bool rt = true)
    {
        MAX_HEIGHT = height;
        rotate = rt;
        parent = creator;
        setSpeed(minSp, maxSp);
        if (rotate)
            setRotation();
    }//end setValues

    private void setSpeed(int minSp, int maxSp)
    {
        if (minSp == maxSp)
            speed = 1;
        else
            speed = Random.Range(minSp, maxSp);
    }//end setSpeed

    private void setRotation()
    {
        float xRotate = 50, yRotate = 50, zRotate = 50;
        //Set xRotation forward/backwards
        if (Random.Range(0, 2) == 0)
            xRotate = xRotate * speed;
        else
            xRotate = xRotate * speed * -1;
        //Set yRotation forward/backwards
        if (Random.Range(0, 2) == 0)
            yRotate = yRotate * speed;
        else
            yRotate = yRotate * speed * -1;
        //Set zRotation forward/backwards
        if (Random.Range(0, 2) == 0)
            zRotate = zRotate * speed;
        else
            zRotate = zRotate * speed * -1;

        rotateVec = new Vector3(xRotate, yRotate, zRotate);
    }//end setRotation

    protected abstract void movement();
    protected abstract float xMove(float time);
    protected abstract float yMove(float time);

}