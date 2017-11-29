using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TARGET: UI manager in the !HUB! area
/// </summary>

public class DisableCameraHub : MonoBehaviour {

    //Script of the camera
    public ThirdPersonCamera thirdPersonCamera;

    //Main pause script attached to UI manager
    private pauseGame pauseScript;

    private void Start()
    {
        pauseScript = GameObject.FindGameObjectWithTag("UIManager").GetComponent<pauseGame>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (pauseScript.isPaused)
            thirdPersonCamera.enabled = false;
        else
            thirdPersonCamera.enabled = true;
	}
}
