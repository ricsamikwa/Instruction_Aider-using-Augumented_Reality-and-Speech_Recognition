using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;


public class PositionDetect : MonoBehaviour {
    public GameObject component;
    public GameObject componentMark;
    Vector3 componentPosition;
    Vector3 componentMarkPosition;
    GameObject testText;
    // Use this for initialization
    void Start () {
        componentPosition = component.transform.position;
        componentMarkPosition = componentMark.transform.localPosition;
        testText = GameObject.Find("Text");
    }
	
	// Update is called once per frame
	void Update () {
        componentPosition = component.transform.position;
        componentMarkPosition = componentMark.transform.position;
        testText.GetComponent<Text>().text = (componentPosition.x.ToString("F3")) +" "+ (componentPosition.y.ToString("F3")) + " "+(componentPosition.z.ToString("F3"))+ "\n"
            + (componentMarkPosition.x.ToString("F3")) + " "+(componentMarkPosition.y.ToString("F3")) + " "+(componentMarkPosition.z.ToString("F3"));
    }
}
