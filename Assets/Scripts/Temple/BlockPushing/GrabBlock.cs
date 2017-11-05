using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBlock : MonoBehaviour {

    public GameObject player;

    public float distance = 5.0f;

    public Transform top;
    public Transform bot;
    public Transform right;
    public Transform left;

    public enum side
    {
        top,
        bot,
        right,
        left, none
    }

    private side pushedSide;

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Interact"))
        {
            ///grab block
            checkBlockSides();
            if (pushedSide != side.none)
                Debug.Log(pushedSide);
            else
                Debug.Log("NONE");

        }
    }

    void checkBlockSides()
    {
        float topD = Vector3.Distance(player.transform.position, top.position);
        float botD = Vector3.Distance(player.transform.position, bot.position);
        float leftD = Vector3.Distance(player.transform.position, left.position);
        float rightD = Vector3.Distance(player.transform.position, right.position);

        float closestWallDistance = Mathf.Min(topD, botD, leftD, rightD);

        if (closestWallDistance < distance)
        {

            if (closestWallDistance == topD)
            {
                pushedSide = side.top;
                return;
            }

            if (closestWallDistance == botD)
            {
                pushedSide = side.bot;
                return;
            }

            if (closestWallDistance == leftD)
            {
                pushedSide = side.left;
                return;
            }

            if (closestWallDistance == rightD)
            {
                pushedSide = side.right;
                return;
            }
        }

        pushedSide = side.none;
    }
}
