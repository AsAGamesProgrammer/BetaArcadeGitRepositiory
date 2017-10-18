using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// APPLIED TO: trigger 
/// DESCRIPTION: On trigger stay listen for a keyboard input. 
/// If input received, swap the cameras (enable one and disable another)
/// </summary>

public class mapCamera : MonoBehaviour {

    private GameObject mainCamera;  //Main camera is found automatically
    public GameObject otherCamera;  //Other camera has to be dragged manually

    private bool isOnTrigger = false;   //Is true when the PLAYER is colliding with a trigger

    // Use this for initialization
    void Start ()
    {
        //Find main camera by tag
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    private void Update()
    {
        if(isOnTrigger)
        {
            if (Input.GetButtonDown("Interact"))
            {
                print("Camera Swap");
                if (mainCamera.activeSelf)
                {
                    mainCamera.SetActive(false);
                    otherCamera.SetActive(true);
                }
                else
                {
                    mainCamera.SetActive(true);
                    otherCamera.SetActive(false);
                }
            }

        }

    }

    //These 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOnTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOnTrigger = false;
        }
    }

    /// <summary>
    /// This piece of code doesn't always work
    /// </summary>
    /// <param name="other"></param>
    //private void OnTriggerStay(Collider other)
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //        print(other.name);

    //    if (other.gameObject.tag == "Player")
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            print("Camera Swap");
    //            if (mainCamera.activeSelf)
    //            {
    //                mainCamera.SetActive(false);
    //                otherCamera.SetActive(true);
    //            }
    //            else
    //            {
    //                mainCamera.SetActive(true);
    //                otherCamera.SetActive(false);
    //            }
    //        }
    //    }
    //}
}
