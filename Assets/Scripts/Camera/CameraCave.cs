using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCave : MonoBehaviour {


    private GameObject player;
    public Vector3 offset = new Vector3();

    public Transform rightWall;
    public Transform leftWall;
    public Transform topWall;
    public Transform botWall;

    private bool closeToTop;
    private bool closeToBot;
    private bool closeToLeft;
    private bool closeToRight;

    public float cameraToSideWall;
    public float cameraToTopWall;

    //Resetting camera
    private Vector3 initialRotation;

    private Vector3 lerpPosition;
    private bool lerpCamera = false;
    int lerpFrames = 0;

    //Falling camera
    public bool TiaIsFalling = false;
    private bool fallTransformSet = false;
    private Vector3 fallPosition;
    private Vector3 fallRotation;
    public float fallingCameraSpeed = 10f;

    //Fixed angle camera
    public float fixedAngleCameraSpeed = 3f;
    private Vector3 fixedCameraDestinationPosition;
    private Quaternion fixedCameraDestinationRotation;
    bool isFixedAngleCamera = false;
    float lerpStage = 0.01f;

    // Use this for initialization
    void Start ()
    {
        //Player game object
        player = GameObject.FindWithTag("Player");

        offset = transform.position - player.transform.position;

        //Rotattion
        initialRotation = transform.localEulerAngles;

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
        else if (isFixedAngleCamera)
        {
            performFixedCameraAngle();
        }
        else
        {
            moveNormally();
        }
    }

    //-----------------------
    //   FALLING CAMERA
    //-----------------------

    void fallingCamera()
    {
        //Set new camera transform
        if (!fallTransformSet)
        {
            fallPosition = new Vector3(transform.position.x + 10, transform.position.y + 3, transform.position.z);
            fallTransformSet = true;
        }

        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, fallRotation, fallingCameraSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, fallPosition, fallingCameraSpeed * Time.deltaTime);
    }

    //-----------------------
    //     FIXED ANGLE
    //-----------------------
    //Moves a camera from the offset position to a fixed object
    //USED: by triggers coverig the area
    public void startFixedAngleCamera(Transform fixedLocation)
    {
        Debug.Log("Start was called");
        fixedCameraDestinationPosition = fixedLocation.position;
        fixedCameraDestinationRotation = fixedLocation.rotation;

        isFixedAngleCamera = true;
    }

    void performFixedCameraAngle()
    {
        //lerpStage += 0.01f;
        //transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, fallRotation, fallingCameraSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, fixedCameraDestinationPosition, fixedAngleCameraSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, fixedCameraDestinationRotation, fixedAngleCameraSpeed * Time.deltaTime);

        //Quaternion

       // transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, fixedCameraDestinationRotation, fixedAngleCameraSpeed * Time.deltaTime);
       
    }

    //-----------------------
    //   NORMAL MOVEMENT
    //-----------------------

    void moveNormally()
    {
        cameraCanMove();

        if (!lerpCamera)
        {

            Vector3 newPosition = player.transform.position;

            if (closeToRight || closeToLeft)
            {
                newPosition.z = transform.position.z - offset.z;
            }

            if (closeToBot || closeToTop)
            {
                newPosition.x = transform.position.x - offset.x;
            }
            transform.position = newPosition + offset;
        }
        else
        {
            if (Vector3.Distance(transform.position, lerpPosition) > 0.6)
            {
                Debug.Log("I called reset position, CameraCave");
                resetPosition();
                transform.position = Vector3.Lerp(transform.position, lerpPosition, Time.deltaTime * 7);
            }
            else
            {
                lerpCamera = false;
            }

        }
    }

    //Checks if camera is not too cloase to the wall
    void cameraCanMove()
    {
        closeToRight = false;
        closeToLeft = false;
        closeToTop = false;
        closeToBot= false;

        //Right
        //float distanceR = Vector3.Distance(player.transform.position, rightWall.position);
        float distanceR = player.transform.position.z - rightWall.position.z;
        if (distanceR < 0)
            distanceR *= -1;
        if (distanceR < cameraToSideWall)
            closeToRight = true;

        //Left
        float distanceL = player.transform.position.z - leftWall.position.z;
        if (distanceL < 0)
            distanceL *= -1;
        if (distanceL < cameraToSideWall)
            closeToLeft = true;

        //Top
        float distanceT = player.transform.position.x - topWall.position.x;
        if (distanceT < 0)
            distanceT *= -1;
        if (distanceT < cameraToTopWall)
            closeToTop = true;

        //Bot
        float distanceB = player.transform.position.x - botWall.position.x;
        if (distanceB < 0)
            distanceB *= -1;
        if (distanceB < cameraToTopWall)
            closeToBot = true;
    }

    //-----------------------
    //   CAMERA RESETS
    //-----------------------
    //Resets both a player and a camera without any animation, disable falls
    //USED: by triggers on the falling platforms
    public void hardReset(Transform resetLocation)
    {
        //Tia is no longer falling
        TiaIsFalling = false;
        fallTransformSet = false; 

        //Reset player
        player.transform.position = resetLocation.position;
        player.transform.rotation = resetLocation.rotation;

        //Reset camera
        transform.position = offset;
        transform.localEulerAngles = initialRotation;
    }

    public void shiftCamera()
    {
        Debug.Log(initialRotation);
        transform.localEulerAngles = initialRotation;
        Debug.Log(transform.localEulerAngles);
        Debug.Log(tag);
        //resetPosition();
        //transform.position = lerpPosition;
    }

    public void resetPosition()
    {
        transform.localEulerAngles = initialRotation;

        Vector3 extraTrasform = new Vector3();

        //Top
        if (closeToTop)
        {
            float distanceT = player.transform.position.x - topWall.position.x;
            if (distanceT < 0)
                distanceT *= -1;

            float difference = cameraToTopWall - distanceT;

            extraTrasform.x = -difference;
        }

        //Bot
        if (closeToBot)
        {
            float distanceB = player.transform.position.x - botWall.position.x;
            if (distanceB < 0)
                distanceB *= -1;

            float differenceB = cameraToTopWall - distanceB;

            extraTrasform.x = differenceB;
        }

        //Left
        if (closeToLeft)
        {
            float distanceL = player.transform.position.z - leftWall.position.z;
            if (distanceL < 0)
                distanceL *= -1;

            float differenceL = cameraToSideWall - distanceL;

            extraTrasform.z = differenceL;
        }

        //Right
        if (closeToRight)
        {
            float distanceR = player.transform.position.z - rightWall.position.z;
            if (distanceR < 0)
                distanceR *= -1;

            float differenceR = cameraToSideWall - distanceR;

            extraTrasform.z = -differenceR;
        }

        
        //transform.position = player.transform.position + offset + extraTrasform;
        lerpPosition = player.transform.position + offset + extraTrasform;
        lerpCamera = true;
        //transform.position = Vector3.Lerp(transform.position, player.transform.position + offset + extraTrasform, Time.deltaTime * 3);



    }
}
