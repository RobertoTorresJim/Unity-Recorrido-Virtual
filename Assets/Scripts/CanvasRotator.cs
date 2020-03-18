using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotator : MonoBehaviour {
    public Camera mainCamera;
    public Camera FPCCamera;
    //public Transform canvasTransform;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (mainCamera.isActiveAndEnabled)
            this.GetComponent<Transform>().rotation = mainCamera.GetComponent<Transform>().rotation;
        else
            this.GetComponent<Transform>().rotation = FPCCamera.GetComponent<Transform>().rotation;
	}
}
