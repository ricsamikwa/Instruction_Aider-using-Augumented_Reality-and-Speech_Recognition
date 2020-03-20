using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

    // Use this for initialization
    public GameObject text;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        text.transform.LookAt(Camera.main.transform.position);
	}
}
