using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushNorth : MonoBehaviour {

    public GrabBlock blockScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            blockScript.setPushedSide(GrabBlock.side.top);
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
