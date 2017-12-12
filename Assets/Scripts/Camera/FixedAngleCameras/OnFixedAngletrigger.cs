using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFixedAngletrigger : MonoBehaviour {

    public Transform newCameraPosition;
    private CameraCave mainCameraScript;

	// Use this for initialization
	void Start ()
    {
        mainCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCave>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainCameraScript.startFixedAngleCamera(newCameraPosition);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainCameraScript.resetCameraToNormal();
        }
    }
}
