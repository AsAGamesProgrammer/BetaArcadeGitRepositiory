using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to the pushable block object
/// </summary>

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

}
