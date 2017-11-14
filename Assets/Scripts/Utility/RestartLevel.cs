using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// TARGET: cube or other object which will collide with the player on pitfall
/// AUTHORS: Craig Belshaw, Kristina Blinova
/// 
/// On pitfall will return player to the initial transform be default or any custom position
/// </summary>

public class RestartLevel : MonoBehaviour {

    //Transforms and positions
    private Transform initialPlayerTransform;   //Automatically detected
    public Transform customTransform;           //Any custom transfor e.g. cube
    private Vector3 afterlifePosition;          //A position which will be used

    //Camera
    private Camera mainCamera;
    public playerSwapCamera swapScript;

    // Use this for initialization
    void Start ()
    {
        //Transforms
        initialPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (customTransform !=null)
        {
            afterlifePosition = customTransform.position;
        }
        else
        {
            afterlifePosition = initialPlayerTransform.position;
        }

        //Camera
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

    }

    //Collsion check
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            //Move the player
            collision.gameObject.transform.position = afterlifePosition;

            //Return camera to a correct position
            mainCamera.GetComponent<CameraCave>().enabled = true;
            mainCamera.GetComponent<CameraCave>().shiftCamera();

            swapScript.useRelocatedCamera = false;
        }
    }
}
