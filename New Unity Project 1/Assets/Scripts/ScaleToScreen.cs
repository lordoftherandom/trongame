using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToScreen : MonoBehaviour {
	public float scaleMultiple;
	public float orgWidth; //In pixels
	public float orgHeight; //In pixels

	private float orgAspect, orgCamSize, currAspect, currWidth, currHeight;
	private GameObject parentObject;

	void Awake(){
		orgAspect = orgWidth / orgHeight;
		orgCamSize = Camera.main.orthographicSize;
		parentObject = GameObject.FindGameObjectWithTag ("Parent");
		currWidth = Screen.width;
		currHeight = Screen.height;
		currAspect = currWidth / currHeight; //If currAspect > orgAspect, too much width. if currAspect < orgAspect, too much height. else, fine
		ResolutionUpdate();
	}
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		if (Screen.width != currWidth || Screen.height != currHeight)
			ResolutionUpdate ();
	}

	public float GetScaleMultiple()
	{
		return scaleMultiple;
	}


	void ResolutionUpdate()
	{
		currWidth = Screen.width;
		currHeight = Screen.height;
		currAspect = currWidth / currHeight;
		print (orgCamSize);
		Camera.main.orthographicSize = orgCamSize;
		if (currAspect > orgAspect) {
			scaleMultiple = currAspect;
			Camera.main.orthographicSize = Camera.main.orthographicSize * orgAspect;
		} else if (currAspect < orgAspect) {//too much height
			scaleMultiple = currAspect;
			Camera.main.orthographicSize = Camera.main.orthographicSize * orgAspect;
		} else {
			float heightScale = (Screen.height / orgHeight);
			float widthScale = (Screen.width / orgWidth);
			if (widthScale < heightScale)
				scaleMultiple = widthScale;
			else
				scaleMultiple = heightScale;
			Camera.main.orthographicSize = Camera.main.orthographicSize * scaleMultiple;
		}
		parentObject.transform.localScale = new Vector3 (scaleMultiple, scaleMultiple, scaleMultiple);
	}
}
