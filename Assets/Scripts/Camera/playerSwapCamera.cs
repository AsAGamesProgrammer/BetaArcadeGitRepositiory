using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSwapCamera : MonoBehaviour {

    public GameObject cameraLocaion;
    public Camera mainCamera;
    private bool useRelocatedCamera = false;

    public float rotationSpeed;
    public float speed;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
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
        Debug.Log(other.tag);
        if (!useRelocatedCamera)
        {
            useRelocatedCamera = true;
            mainCamera.GetComponent<CameraCave>().enabled = false;
        }
        else
        {
            useRelocatedCamera = false;
            mainCamera.GetComponent<CameraCave>().enabled = true;

            mainCamera.GetComponent<CameraCave>().resetPosition();

            //Lerp between two positions
            //mainCamera.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;

            //Lerp between two rotations
           // mainCamera.transform.rotation = initialTransform.rotation;
           // Debug.Log(initialTransform.rotation);

        }
    }
}
