using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDestination : MonoBehaviour {

    public int id = 0;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            other.gameObject.GetComponent<cameraOnrails>().changeDestination(id);
        }
    }
};
