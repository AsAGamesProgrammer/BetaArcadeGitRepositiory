using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

/// <summary>
/// TARGET: any empty game object
/// Script pauses the game but not the camera. Use specific camera scripts for this task
/// </summary>

public class pauseGame : MonoBehaviour {

    public Player playerScript;
    public GameObject pauseMenu;

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
        PauseGame();
	}

    private void PauseGame()
    {
        pauseMenu.SetActive(isPaused);

        playerScript.SetIgnoreInput(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;
            //SET GUI BUTTON HIGHLIGHTED
        }
        else
            Time.timeScale = 1f;
    }

    //Button clicks
    public void onResumeClick()
    {
        isPaused = false;
    }

    public void onMenuClick()
    {
        isPaused = false;
        PauseGame();
        EditorSceneManager.LoadScene(0);
    }

    
}
