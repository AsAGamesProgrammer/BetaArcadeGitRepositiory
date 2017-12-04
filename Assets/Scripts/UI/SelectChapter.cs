using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SelectChapter : MonoBehaviour {

    //Transforms of Tia
    public Transform hubTransform;
    public Transform caveTransform;
    public Transform templeTransform;
    public GameObject Tiare;

    private Transform[] destinations;
    private int currentLocation = 1;

    private const float constTime = 0.13f;
    private float inputTimer;

    //Audio clips for each scene
    public AudioMixerSnapshot hubMusic;
    public AudioMixerSnapshot caveMusic;
    public AudioMixerSnapshot templeMusic;
    private AudioMixerSnapshot[] audioSources;

  // Use this for initialization
  void Start ()
    {
        setTia(hubTransform);

        //Array of destinations in the correct order
        destinations = new Transform[3];
        destinations[0] = hubTransform;
        destinations[1] = caveTransform;
        destinations[2] = templeTransform;

        //Array of audio clis
        audioSources = new AudioMixerSnapshot[3];
        audioSources[0] = hubMusic;
        audioSources[1] = caveMusic;
        audioSources[2] = templeMusic;

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
            if (inputAxis > 0.2f)
            {
                //Stop music
                //audioSources[currentLocation-1].T();

                //Check against max and min possible index
                if (currentLocation < 3)
                    currentLocation++;
                else
                    currentLocation = 1;

                //Move character
                setTia(destinations[currentLocation - 1]);
                var gObject = GameObject.FindGameObjectWithTag("Click");
                var audio = gObject.GetComponent<AudioSource>();
                audio.Play();
                //Pla music music
                audioSources[currentLocation - 1].TransitionTo((60/128)*10);

            }

            //Input "LEFT"
            if (inputAxis < -0.2f)
            {
                //Stop music
                //audioSources[currentLocation - 1].Stop();

                //Check against max and min possible index
                if (currentLocation > 1)
                    currentLocation--;
                else
                    currentLocation = 3;

                //Move character
                setTia(destinations[currentLocation - 1]);
                var gObject = GameObject.FindGameObjectWithTag("Click");
                var audio = gObject.GetComponent<AudioSource>();
                audio.Play();
                //Play music
                audioSources[currentLocation - 1].TransitionTo((60 / 128) * 10);
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
