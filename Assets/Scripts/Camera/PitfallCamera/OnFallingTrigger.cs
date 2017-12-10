using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TARGET: trigger which detemines tht the player started falling
//Must be a trigger

public class OnFallingTrigger : MonoBehaviour {

    CameraCave mainCameraScript;

	// Use this for initialization
	void Start ()
    {
        mainCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCave>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainCameraScript.TiaIsFalling = true;
        }
    }
}
