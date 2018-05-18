using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {
	private bool rotate;
	private Vector3 rotateVec;
	protected float MAX_HEIGHT;
	protected float speed;
	protected GameObject parent;


	void Start () {

	}//end start

	public void setValues(float height, float sp, GameObject creator, bool rt = true)
	{
		MAX_HEIGHT = height;
		speed = sp;
		rotate = rt;
		parent = creator;
		if (rotate)
			setRotation ();
	}
	
	// Update is called once per frame
	void Update () {
		float time = Time.deltaTime;
		if (rotate)
			transform.Rotate (rotateVec * time);
	}

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

	private void setRotation()
	{
		float xRotate = 50, yRotate = 50, zRotate = 50;
		float temp = speed; 
		if (Random.Range (0, 2) == 0)
			xRotate = xRotate * temp;
		else
			xRotate = xRotate * temp * -1;
		if (Random.Range (0, 2) == 0)
			yRotate = yRotate * temp;
		else
			yRotate = yRotate * temp * -1;
		if (Random.Range (0, 2) == 0)
			zRotate = zRotate * temp;
		else
			zRotate = zRotate * temp * -1;

		rotateVec = new Vector3 (xRotate, yRotate, zRotate);
	}

}
