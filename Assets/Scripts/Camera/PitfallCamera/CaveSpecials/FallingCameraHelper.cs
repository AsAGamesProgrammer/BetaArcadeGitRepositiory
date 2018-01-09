using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCameraHelper : MonoBehaviour {

    //Falling camera
    public bool TiaIsFalling = false;
    private bool fallTransformSet = false;
    private Vector3 fallPosition;
    private Vector3 fallRotation;
    public float fallingCameraSpeed = 10f;
    public GameObject Tia;

    // Use this for initialization
    void Start ()
    {
        //Fall transform rotation
        fallPosition = new Vector3();
        fallRotation = new Vector3(90, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (TiaIsFalling)
        {
            //Falling camera
            fallingCamera();
        }
    }

    void fallingCamera()
    {
        //Set new camera transform
        if (!fallTransformSet)
        {
            fallPosition = new Vector3(Tia.transform.position.x-5, Tia.transform.position.y + 3, Tia.transform.position.z);
            fallTransformSet = true;
        }

        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, fallRotation, fallingCameraSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, fallPosition, fallingCameraSpeed * Time.deltaTime);
    }
}
