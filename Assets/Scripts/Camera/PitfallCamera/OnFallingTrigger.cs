using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TARGET: trigger which detemines tht the player started falling
//Must be a trigger

public class OnFallingTrigger : MonoBehaviour {

    public bool usesThirdPersonCamera = false;

    CameraCave mainCameraScript;
    public ThirdPersonCamera thirdPerson;

	// Use this for initialization
	void Start ()
    {
        if (!usesThirdPersonCamera)
            mainCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCave>();

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!usesThirdPersonCamera)
                mainCameraScript.TiaIsFalling = true;
            else
            {
                thirdPerson.enabled = false;
            }
        }
    }
}
