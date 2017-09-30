using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraOnrails : MonoBehaviour {

    //--------VARIABLES---------
    public GameObject destinationCollection;    //Collection of the  game objects representing destinations
    private Transform[] destinations;           //Array of transforms, which serve as destinations

    public int nextDestination =0;             //Indicates a destination camera is moving towards
    public int previousDestination = -1;

    public float rotationSpeed = 2.0f;          //Speed of quaternion lerp used in rotation

    bool movingForwards = false;                //Indicates if a player is moving forward
    bool movingBackwards = false;               //Indicates if a player is moving backwards

    //-------CONSTRUCTOR--------
    private void Start()
    {
        //Define size of the "destination" array
        int arrayLength = destinationCollection.transform.childCount;
        destinations = new Transform[arrayLength];

        //Populate "destination" array
        int nextObject = 0;
        foreach (Transform child in destinationCollection.transform)
        {
            child.GetComponent<cameraDestination>().id = nextObject;
            destinations[nextObject] = child;
            nextObject++;
        }
    }

    //------FUNCTIONALITY--------

    //Changes destination of the camera, is called by destination prefabs
    public void changeDestination(int id)
    {
        if (movingForwards)
        {
            nextDestination = id + 1;
            previousDestination = id;
        }
        else
        {
            nextDestination = id;
            previousDestination = id-1;
        }
    }
	
	// Update, currently handles player input
	void Update ()
    {
        if (Input.GetKey("up"))
        {
            MoveForward();
        }

        if (Input.GetKey("down"))
        {
            MoveBackwards();
        }

	}

    //Move to any destination
    void MoveToDestination(int destinationIdx)
    {
        //Lerp between two positions
        transform.position = Vector3.Lerp(transform.position, destinations[destinationIdx].transform.position, Time.deltaTime);

        //Lerp between two rotations
        transform.rotation = Quaternion.Lerp(transform.rotation, destinations[destinationIdx].transform.rotation, Time.deltaTime * rotationSpeed);
    }

    //Move forward to the next position
    void MoveForward()
    {
        //Change moving direction
        movingBackwards = false;
        movingForwards = true;

        //Move to a direction
        if (nextDestination<destinations.Length)
            MoveToDestination(nextDestination);
    }

    //Move back to the previous position
    void MoveBackwards()
    {
        //Change moving direction
        movingBackwards = true;
        movingForwards = false;

        //Move to a direction
        if (previousDestination >-1)
            MoveToDestination(previousDestination);
    }
}
