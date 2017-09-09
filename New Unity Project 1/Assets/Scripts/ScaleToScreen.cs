using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToScreen : MonoBehaviour {
	public float scaleMultiple;
	public float orgWidth; //In pixels
	public float orgHeight; //In pixels

	private float orgAspect;
	private GameObject parentObject;

	// Use this for initialization
	void Start () {
		orgWidth = 800.0f;
		orgHeight = 480.0f;
		print (Screen.height);
		print (Screen.width);

		float heightScale = (Screen.height / orgHeight);
		float widthScale = (Screen.width / orgWidth);

		scaleMultiple = widthScale;
		parentObject = GameObject.FindGameObjectWithTag ("Parent");
		print (parentObject.tag);
		print (scaleMultiple);
		parentObject.transform.localScale = new Vector3 (scaleMultiple, scaleMultiple, parentObject.transform.localScale.z);
		Camera.main.orthographicSize = Camera.main.orthographicSize * scaleMultiple;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
