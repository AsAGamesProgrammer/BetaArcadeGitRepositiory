using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushSouth : MonoBehaviour {

    public GrabBlock blockScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            blockScript.setPushedSide(GrabBlock.side.bot);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            blockScript.setPushedSide(GrabBlock.side.none);
        }
    }
}
