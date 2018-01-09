using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TARGET: trigger which detemines that the player was falling for long enough and now can revive
//Must be a trigger

public class OnresetTrigger : MonoBehaviour {

    public GameObject resetTarget;
    public GameObject Tia;
    CameraCave mainCameraScript;

    public bool usesThirdPersonCamera = false;
    public ThirdPersonCamera thirdPerson;

    // Use this for initialization
    void Start ()
    {
        if (!usesThirdPersonCamera)
            mainCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCave>();
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
