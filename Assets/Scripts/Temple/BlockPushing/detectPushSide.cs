using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPushSide : MonoBehaviour {

    private pushableBlock parentScript;

    private void Start()
    {
        parentScript = transform.parent.gameObject.GetComponent<pushableBlock>();
    }

    //On contact
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Contacted");

        if (other.tag == "Player")
          parentScript.contactFrom(this.name, true);    //Send msg to parent
    }

    //On loosing contact
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            parentScript.contactFrom(this.name, false); //Send msg to parent
    }
}
