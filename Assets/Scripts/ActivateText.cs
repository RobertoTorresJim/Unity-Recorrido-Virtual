using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateText : MonoBehaviour {
    public GameObject image;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other){
        //this.GetComponent<MeshRenderer>().enabled = true;
        image.GetComponent<SpriteRenderer>().enabled = true;
        image.GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
       // this.GetComponent<MeshRenderer>().enabled = false;
        image.GetComponent<SpriteRenderer>().enabled = false;
        image.GetComponentInChildren<MeshRenderer>().enabled = false;
    }
}
