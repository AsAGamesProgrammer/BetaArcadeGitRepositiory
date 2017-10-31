using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {
  public Transform playertransform;
	// Use this for initialization
	void Start () {
		
	}

  // Update is called once per frame
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.collider.CompareTag("Player"))
    {
      collision.gameObject.transform.position = playertransform.position;
    }
      //collision.transform.position = ;
  }
}
