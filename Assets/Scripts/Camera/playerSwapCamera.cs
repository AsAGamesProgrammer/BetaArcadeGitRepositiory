using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSwapCamera : MonoBehaviour {

    public GameObject cameraLocaion;
    private Camera mainCamera;
    private bool useRelocatedCamera = false;

    public float rotationSpeed;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        //Find main camera by tag
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //If camera has to be moved 
        if (useRelocatedCamera)
        {
            //Lerp between two positions
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraLocaion.transform.position, Time.deltaTime * speed);

            //Lerp between two rotations
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, cameraLocaion.transform.rotation, Time.deltaTime * rotationSpeed);
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (!useRelocatedCamera && other.gameObject.tag=="Player")
        {
            useRelocatedCamera = true;
            mainCamera.GetComponent<CameraCave>().enabled = false;
        }
        else
        {
            useRelocatedCamera = false;
        }
    }
}
