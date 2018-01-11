using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// TARGET: any empty game object
/// Script pauses the game but not the camera. Use specific camera scripts for this task
/// </summary>

public class pauseGame : MonoBehaviour {

	public UnityEngine.UI.Button startButton;

    public bool canBePaused = true;

    public Player playerScript;
    private GameObject pauseMenu;

    public bool isPaused = false;
    private bool menuIsVisible = false;

    //Initial menu transform
    // A reason for this peculiar way of disabling a menu is a unity bug
    // After enabling a button it is selected but not highlighted
    private Vector3 pauseMenuInitialPos;
    private Vector3 pauseMenuUnseenPos;

    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenuInitialPos = pauseMenu.transform.position;
        pauseMenuUnseenPos.y = pauseMenuInitialPos.y + 1000;
    }

    // Update is called once per frame
    void Update ()
    {
        //If there is an option to pause the game
        if (canBePaused)
        {
            //Get input for a pause
            if (Input.GetButtonDown("Pause"))
            {
				startButton.Select ();
                isPaused = !isPaused;
                Debug.Log("Game paused " + isPaused);
            }

            //Handle a pause
			PauseGame();
        }
    }

    private void PauseGame()
    {
        //Show menu
        showMenu(isPaused);

        //Change time
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
            Time.timeScale = 1f;

        //Disable player's input
        //playerScript.SetIgnoreInput(isPaused);
    }

    /// <summary>
    /// A reason for this peculiar way of disabling a menu is a unity bug
    /// After enabling a button it is selected but not highlighted
    /// </summary>
    /// <param name="toShow"></param>
    public void showMenu(bool toShow)
    {
        if (toShow)
        {
            pauseMenu.transform.position = pauseMenuInitialPos;
            menuIsVisible = true;

        }
        else
        {
            pauseMenu.transform.position = pauseMenuUnseenPos;
            menuIsVisible = false;
        }

    }

    //------------BUTTON CLICKS---------------
    public void onResumeClick()
    {
        if(menuIsVisible)
            isPaused = false;
    }

    public void onMenuClick()
    {
        if (menuIsVisible)
        {
            isPaused = false;
            PauseGame();
            SceneManager.LoadScene(0);
        }
    }



}
