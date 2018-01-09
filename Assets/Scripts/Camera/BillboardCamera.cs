using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardCamera : MonoBehaviour {

    public GameObject paintingCamera;
    public GameObject mainCamera;

    GameObject player;

    bool canSeePainting = false;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (canSeePainting)
            {

                Debug.Log("Can see painting");
                if (mainCamera.activeSelf)
                {
                    paintingCamera.SetActive(true);
                    mainCamera.SetActive(false);
                    player.GetComponent<Player>().SetIgnoreInput(true);
                }
                else
                {
                    paintingCamera.SetActive(false);
                    mainCamera.SetActive(true);
                    player.GetComponent<Player>().SetIgnoreInput(false);
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canSeePainting = true;
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canSeePainting = false;
        }
    }
}
