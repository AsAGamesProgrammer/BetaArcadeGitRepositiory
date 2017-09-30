using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDestination : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            other.gameObject.GetComponent<cameraOnrails>().changeDestination();
        }
    }
}
