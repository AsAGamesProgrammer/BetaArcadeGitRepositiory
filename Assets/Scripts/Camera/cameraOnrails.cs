using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraOnrails : MonoBehaviour {

    //--------VARIABLES---------
    public GameObject destinationCollection;    //Collection of the  game objects representing destinations
    private Transform[] destinations;           //Array of transforms, which serve as destinations

    private int nextDestination =0;             //Indicates a destination camera is moving towards
    private bool canMove = true;                //Indicates if camera can move

    public float rotationSpeed = 2.0f;          //Speed of quaternion lerp used in rotation

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
            destinations[nextObject] = child;
            nextObject++;
        }
    }

    //------FUNCTIONALITY--------

    //Changes destination of the camera, is called by destination prefabs
    public void changeDestination()
    {
        if (destinations.Length > nextDestination + 1)
        {
            nextDestination++;
        }
        else
            canMove = false;
    }
	
	// Update, currently handles player input
	void Update ()
    {
        if (Input.GetKey("space"))
        {
            if (canMove)
                MoveForward();
        }

	}

    //Move forward to the next position
    void MoveForward()
    {
        transform.position = Vector3.Lerp(transform.position, destinations[nextDestination].transform.position, Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, destinations[nextDestination].transform.rotation, Time.deltaTime * rotationSpeed);
    }

    //Move back to the previous position
    void MoveBackwards()
    {

    }
}
