using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMinimap : MonoBehaviour {

    public GameObject minimap;
    public bool mapShown;

    //This script should move a camera for a mini map
	
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
