using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectChapter : MonoBehaviour {

    public Transform hubTransform;
    public Transform caveTransform;
    public Transform templeTransform;
    public GameObject Tiare;

    private Transform[] destinations;
    private int currentLocation = 1;

    private const float constTime = 0.13f;
    private float inputTimer;

	// Use this for initialization
	void Start ()
    {
        setTia(hubTransform);

        //Array of destinations in the correct order
        destinations = new Transform[3];
        destinations[0] = hubTransform;
        destinations[1] = caveTransform;
        destinations[2] = templeTransform;

        //Timer
        inputTimer = constTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Move tia to select a scene
        moveTiaOnUpdate();

        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(currentLocation);
        }

    }

    private void moveTiaOnUpdate()
    {
        //Check timer to see if the next input can be taken
        if (inputTimer <= 0)
        {
            //Reset timer
            inputTimer = constTime;

            //Get input
            float inputAxis = Input.GetAxis("Horizontal");

            //Input "RIGHT"
            if (inputAxis > 0.5f)
            {
                //Check against max and min possible index
                if (currentLocation < 3)
                    currentLocation++;
                else
                    currentLocation = 1;

                //Move character
                setTia(destinations[currentLocation - 1]);
            }

            //Input "LEFT"
            if (inputAxis < -0.5f)
            {
                //Check against max and min possible index
                if (currentLocation > 1)
                    currentLocation--;
                else
                    currentLocation = 3;

                //Move character
                setTia(destinations[currentLocation - 1]);
            }
        }
        else
        {
            //Reduce time until next input
            inputTimer -= Time.deltaTime;
        }
    }

    //Move Tia to a location
    private void setTia(Transform destination)
    {
        Tiare.transform.position = destination.position;
        Tiare.transform.rotation = destination.rotation;
    }
}
