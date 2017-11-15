using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushWest : MonoBehaviour {

    public GrabBlock blockScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            blockScript.setPushedSide(GrabBlock.side.left);
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
