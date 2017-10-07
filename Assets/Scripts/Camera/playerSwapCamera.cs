using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSwapCamera : MonoBehaviour {

    public GameObject cameraLocaion;
    public Camera mainCamera;
    private bool useRelocatedCamera = false;

    private Transform initialTransform;

    public float rotationSpeed;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        initialTransform = mainCamera.transform;
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
        else
        {
            //Lerp between two positions
            //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, initialTransform.position, Time.deltaTime);

            //Lerp between two rotations
            //mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, initialTransform.rotation, Time.deltaTime);

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
        //else
        //{
        //    useRelocatedCamera = false;
        //    mainCamera.GetComponent<CameraCave>().enabled = true;
        //}
    }
}
