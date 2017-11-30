using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwapTopDown : MonoBehaviour {

    private Camera mainCamera;

	// Use this for initialization
	void Start ()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainCamera.GetComponent<CameraCave>().enabled = true;

            mainCamera.GetComponent<CameraCave>().resetPosition();
        }
    }
}
