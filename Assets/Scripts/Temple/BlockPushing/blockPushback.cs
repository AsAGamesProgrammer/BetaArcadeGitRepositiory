using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TARGET: invisible bareers which blocks are not supposed to pass
/// </summary>

public class blockPushback : MonoBehaviour {

    int knockbackSpeed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger "+other.gameObject.tag);

        if (other.gameObject.tag == "O" ||
            other.gameObject.tag == "T" ||
            other.gameObject.tag == "X" ||
            other.gameObject.tag == "C")

        {
            GrabBlock blockScript = other.gameObject.GetComponent<GrabBlock>();
            other.gameObject.transform.position = - knockbackSpeed * blockScript.pushDestination;
        }

    }
}
