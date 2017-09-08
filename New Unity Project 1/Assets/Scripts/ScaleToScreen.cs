using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToScreen : MonoBehaviour {
	public float scaleMultiple;
	public int orgWidth; //In pixels
	public int orgHeight; //In pixels

	private float orgAspect;
	private GameObject parentObject;

	// Use this for initialization
	void Start () {
		orgHeight = 800;
		orgWidth = 480;
		print (Screen.height);
		print (Screen.width);

		float heightScale = (Screen.height / orgHeight);
		float widthScale = (Screen.width / orgWidth);

		if (heightScale > widthScale) {
			scaleMultiple = widthScale;
		
		} else {
			scaleMultiple = heightScale;
		}

		parentObject = GameObject.FindGameObjectWithTag ("Parent");
		print (parentObject.tag);
		parentObject.transform.localScale = new Vector3 (scaleMultiple, scaleMultiple, parentObject.transform.localScale.z);
		Camera.main.orthographicSize = (Camera.main.orthographicSize * scaleMultiple);
		print (Camera.main.orthographicSize);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
