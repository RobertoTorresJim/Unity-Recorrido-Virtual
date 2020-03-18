using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitTrigger : MonoBehaviour {
    private Transform myTransform;
	// Use this for initialization
	void Start () {
        myTransform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(new Vector3 (myTransform.position.x, GetComponent<MeshRenderer>().bounds.extents.y/2.0f, myTransform.position.z) , new Vector3(myTransform.position.x, GetComponent<MeshRenderer>().bounds.extents.y / 2.0f, myTransform.position.z) + (Vector3.forward * -0.8f));
        Debug.DrawLine(new Vector3(1, GetComponent<MeshRenderer>().bounds.extents.y / 2.0f, myTransform.position.z), new Vector3(1, GetComponent<MeshRenderer>().bounds.extents.y / 2.0f, myTransform.position.z) + (Vector3.forward * -0.8f));
        //Debug.DrawLine(new Vector3(myTransform.position.x, GetComponent<MeshRenderer>().bounds.extents.y / 2.0f, myTransform.position.z), new Vector3(myTransform.position.x, GetComponent<MeshRenderer>().bounds.extents.y / 2.0f, myTransform.position.z) + (Vector3.forward * -0.8f));
        //Debug.DrawLine(new Vector3(myTransform.position.x, GetComponent<MeshRenderer>().bounds.extents.y / 2.0f, myTransform.position.z), new Vector3(myTransform.position.x, GetComponent<MeshRenderer>().bounds.extents.y / 2.0f, myTransform.position.z) + (Vector3.forward * -0.8f));
    }
}
