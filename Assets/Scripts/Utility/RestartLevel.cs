using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// TARGET: cube or other object which will collide with the player on pitfall
/// AUTHORS: Craig Belshaw, Kristina Blinova
/// 
/// On pitfall will return player to the initial transform be default or any custom position
/// </summary>

public class RestartLevel : MonoBehaviour {

    private Transform initialPlayerTransform;   //Automatically detected
    public Transform customTransform;           //Any custom transfor e.g. cube

    private Vector3 afterlifePosition;          //A position which will be used

	// Use this for initialization
	void Start ()
    {
        initialPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (customTransform !=null)
        {
            afterlifePosition = customTransform.position;
        }
        else
        {
            afterlifePosition = initialPlayerTransform.position;
        }

    }

    //Collsion check
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Checked collision");
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = afterlifePosition;
        }
    }
}
