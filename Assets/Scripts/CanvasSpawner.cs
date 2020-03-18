using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSpawner : MonoBehaviour {
    bool collision;
    public GameObject canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1") && collision)
        {
            canvas.SetActive(true);
        }
        if(Input.GetButtonDown("Fire2") && collision)
        {
            canvas.SetActive(false);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collision = true;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        collision = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            collision = true;
        }
    }
}
