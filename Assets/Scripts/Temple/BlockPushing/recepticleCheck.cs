using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TARGET: Panel which a block has to be put on
/// PREREQUISITES: Panel should have an identical tag as a matching block
/// </summary>

public class recepticleCheck : MonoBehaviour {

    private bool isCorrect = false;

    public bool getIsCorrect() { return isCorrect; }
	
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag == tag)
        {
            Debug.Log(tag + "correct");
            isCorrect = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == tag)
        {
            isCorrect = false;
        }
    }
}
