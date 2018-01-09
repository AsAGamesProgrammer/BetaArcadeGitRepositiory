using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TARGET: trigger which detemines that the player was falling for long enough and now can revive
//Must be a trigger

public class OnresetTrigger : MonoBehaviour {

    //Flag to see what camera is used in the level
    public bool usesThirdPersonCamera = false;

    //Reset position
    public GameObject resetTarget;

    //Third person camera as default
    GameObject Tia;
    ThirdPersonCamera thirdPerson;

    //Camera cave as default
    CameraCave mainCameraScript;

    // Use this for initialization
    void Start ()
    {
        if (!usesThirdPersonCamera)
            mainCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCave>();
        else
        {
            Tia = GameObject.FindGameObjectWithTag("Player");
            thirdPerson = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ThirdPersonCamera>();
        }
    }
	

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!usesThirdPersonCamera)
                mainCameraScript.hardReset(resetTarget.transform);
            else
            {
                Tia.transform.position = resetTarget.transform.position;
                Tia.transform.rotation = resetTarget.transform.rotation;
                thirdPerson.enabled = true;

            }
        }
    }

}
