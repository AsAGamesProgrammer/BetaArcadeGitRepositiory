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


    // Use this for initialization
    void Start ()
    {
        //Player game object
        player = GameObject.FindWithTag("Player");

        offset = transform.position - player.transform.position;

        //Rotattion
        initialRotation = transform.localEulerAngles;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        cameraCanMove();

        Vector3 newPosition = player.transform.position;

        if (closeToRight || closeToLeft)
        {
            newPosition.x = transform.position.x-offset.x; 
        }

        if (closeToBot || closeToTop)
        {
            newPosition.z = transform.position.z - offset.z;
        }


        transform.position = newPosition + offset;
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
        float distanceR = player.transform.position.x - rightWall.position.x;
        if (distanceR < 0)
            distanceR *= -1;
        if (distanceR < cameraToSideWall)
            closeToRight = true;

        //Left
        float distanceL = player.transform.position.x - leftWall.position.x;
        if (distanceL < 0)
            distanceL *= -1;
        if (distanceL < cameraToSideWall)
            closeToLeft = true;

        //Top
        float distanceT = player.transform.position.z - topWall.position.z;
        if (distanceT < 0)
            distanceT *= -1;
        if (distanceT < cameraToTopWall)
            closeToTop = true;

        //Bot
        float distanceB = player.transform.position.z - botWall.position.z;
        if (distanceB < 0)
            distanceB *= -1;
        if (distanceB < cameraToTopWall)
            closeToBot = true;
    }

    public void resetPosition()
    {
        transform.localEulerAngles = initialRotation;

        Vector3 extraTrasform = new Vector3();

        //Top
        if (closeToTop)
        {
            float distanceT = player.transform.position.z - topWall.position.z;
            if (distanceT < 0)
                distanceT *= -1;

            float difference = cameraToTopWall - distanceT;

            extraTrasform.z = -difference;
        }

        //Bot
        if (closeToTop)
        {
            float distanceB = player.transform.position.z - botWall.position.z;
            if (distanceB < 0)
                distanceB *= -1;

            float differenceB = cameraToTopWall - distanceB;

            extraTrasform.z = differenceB;
        }

        //Left
        if (closeToLeft)
        {
            float distanceL = player.transform.position.x - leftWall.position.x;
            if (distanceL < 0)
                distanceL *= -1;

            float differenceL = cameraToSideWall - distanceL;

            extraTrasform.x = differenceL;
        }

        //Right
        if (closeToRight)
        {
            float distanceR = player.transform.position.x - rightWall.position.x;
            if (distanceR < 0)
                distanceR *= -1;

            float differenceR = cameraToSideWall - distanceR;

            extraTrasform.x = -differenceR;
        }

        transform.position = player.transform.position + offset + extraTrasform;


       
    }
}
