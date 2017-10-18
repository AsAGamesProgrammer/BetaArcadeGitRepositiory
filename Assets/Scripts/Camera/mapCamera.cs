using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// !!!! SUPPORTS KEYBOARD ONLY !!!!
/// </summary>

public class mapCamera : MonoBehaviour {

    private GameObject mainCamera;
    public GameObject otherCamera;

    // Use this for initialization
    void Start ()
    {
        //Find main camera by tag
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            print("E Pressed");
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
            print(other.name);

        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
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
}
