using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TARGET: any empty game object
/// Script pauses the game but not the camera. Use specific camera scripts for this task
/// </summary>

public class pauseGame : MonoBehaviour {

    private bool isPaused = false;
	
	// Update is called once per frame
	void Update ()
    {
        //Get input for a pause
        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
            Debug.Log("Game paused " + isPaused);
        }

        //Handle a pause
        if (isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
	}
}
