using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour {
  public Vector3 movement;
	// Use this for initialization
	void Start () {
		
	}

  void OnTriggerEnter(Collider other)
  {

    if (other.gameObject.tag == "Player")
    {
      other.transform.parent = transform;

    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      other.transform.parent = null;

    }
  }

  // Update is called once per frame
  void FixedUpdate () {
    transform.position+=movement * Mathf.Cos(Time.time) * Time.deltaTime;
  }
}
