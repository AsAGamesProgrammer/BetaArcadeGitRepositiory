using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach script to a block
/// </summary>

public class GrabBlock : MonoBehaviour {

    private GameObject player;

    public float distance = 5.0f;

    private bool isAttached = false;

    public int pushingSpeed = 3;
    public Vector3 pushDestination;

    public enum side
    {
        top,
        bot,
        right,
        left, none
    }

    private side pushedSide = side.none;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Interact"))
        {

            if (isAttached)
            {
                isAttached = false;
                player.transform.parent = null;
                FindObjectOfType<Player>().SetIgnoreInput(false);
            }
            else
            {
                if (pushedSide != side.none)
                {
                    checkPushedDirection();

                    isAttached = true;
                    player.transform.parent = transform;
                    FindObjectOfType<Player>().SetIgnoreInput(true);
                }
            }
        }

        if (isAttached)
        {
            //Push
			float inputAxis = 0.0f;

			if(pushedSide == side.left) 
				inputAxis = Input.GetAxis("Horizontal");

			if(pushedSide == side.right) 
				inputAxis = - Input.GetAxis("Horizontal");
			
			if(pushedSide == side.top) 
				inputAxis = -Input.GetAxis("Vertical");
			
			if(pushedSide == side.bot) 
				inputAxis = Input.GetAxis("Vertical");

			//FORWARD
			if (inputAxis > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, pushDestination + transform.position, Time.deltaTime * pushingSpeed);
            }

			//BACKWARDS
			if(inputAxis < -0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, -pushDestination + transform.position, Time.deltaTime * pushingSpeed);
            }

            FindObjectOfType<Player>().SetPushPullSpeed((pushedSide == side.top || pushedSide == side.bot) ? inputAxis : -inputAxis);
        }			
    }

    public void setPushedSide(side newSide)
    {
        pushedSide = newSide;
    }

    void checkPushedDirection()
    {
            if (pushedSide == side.top)
            {
                pushDestination = Vector3.left;
                return;
            }

            if (pushedSide == side.bot)
            {
                pushDestination = Vector3.right;
                return;
            }

            if (pushedSide == side.left)
            {
                pushDestination = Vector3.back;
                return;
            }

            if (pushedSide == side.right)
            {
                pushDestination = Vector3.forward;
                return;
            }
    }
}
