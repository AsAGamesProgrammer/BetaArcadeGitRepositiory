using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHubDoor : MonoBehaviour {

    public float liftSpeed = 3.0f;
    public bool liftDoor = false;

    //private Vector3 doorHighestPoint;

	// Use this for initialization
	void Start ()
    {
       // doorHighestPoint = new Vector3(transform.position.x, topYPos, transform.position.z);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(liftDoor)
        {
            //Debug.Log(transform.localPosition.y);
            if (transform.localPosition.y < 0)
                transform.Translate(Vector3.forward * Time.deltaTime * liftSpeed);
        }

    }
}
