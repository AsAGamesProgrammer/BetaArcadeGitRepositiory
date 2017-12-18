using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TARGET: trigger which detemines that the player was falling for long enough and now can revive
//Must be a trigger

public class OnresetTrigger : MonoBehaviour {

    public GameObject resetTarget;
    CameraCave mainCameraScript;

    // Use this for initialization
    void Start ()
    {
        mainCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCave>();
    }
	

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Colliding");
            mainCameraScript.hardReset(resetTarget.transform);
        }
    }

}
