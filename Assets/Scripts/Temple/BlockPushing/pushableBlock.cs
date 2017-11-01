using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableBlock : MonoBehaviour {

    private bool westPushed = false;
    private bool eastPushed = false;
    private bool southPushed = false;
    private bool northPushed = false;

    public const int distanceModifier = 1;
    public const int pushingSpeed = 3;

    Vector3 pushDestination;

    // Use this for initialization
    void Start ()
    {
        pushDestination = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetButton("Interact"))
        {
            if (eastPushed)
                pushDestination = transform.position + distanceModifier * Vector3.right;

            if (westPushed)
                pushDestination = transform.position - distanceModifier * Vector3.right;

            if (northPushed)
                pushDestination = transform.position + distanceModifier * Vector3.back;

            if (southPushed)
                pushDestination = transform.position + distanceModifier * Vector3.forward;


        }

        pushBlock(pushDestination);

    }

    //Push a block to a destination
    void pushBlock(Vector3 destination)
    {
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * pushingSpeed);
    }

    //Detects which side the contact comes from
    public void contactFrom(string side, bool isContact)
    {
        if (!isContact)
        {
            resetAll();
            return;
        }

        switch (side)
        {
            case "West":
                resetAll();
                westPushed = true;
                break;

            case "East":
                resetAll();
                eastPushed = true;
                break;

            case "North":
                resetAll();
                northPushed = true;
                break;

            case "South":
                resetAll();
                southPushed = true;
                break;
        }

    }

    private void resetAll()
    {
        westPushed = false;
        eastPushed = false;
        northPushed = false;
        southPushed = false;
    }
}
