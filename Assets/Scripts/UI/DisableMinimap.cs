using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMinimap : MonoBehaviour {

    public GameObject minimap;
    private bool mapShown = false;

    //This script should move a camera for a mini map

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Minimap"))
        {
            if (!mapShown)
                mapShown = true;
            else
                mapShown = false;

            //Set map active/inactive
            minimap.SetActive(mapShown);

        }
    }
}
