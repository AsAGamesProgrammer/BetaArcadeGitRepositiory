using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraOnrails : MonoBehaviour {

    public GameObject[] destinations;

    private int nextDestination =0;
    private bool canMove = true;


    public void changeDestination()
    {
        if (destinations.Length > nextDestination + 1)
        {
            nextDestination++;
            transform.rotation = destinations[nextDestination].transform.rotation;
        }
        else
            canMove = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("space"))
        {
            if (canMove)
                MoveForward();
        }

	}

    //Move to the next position
    void MoveForward()
    {
        transform.position = Vector3.Lerp(transform.position, destinations[nextDestination].transform.position, Time.deltaTime);
    }

    //Move to the previous position
    void MoveBackwards()
    {

    }
}
