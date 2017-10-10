using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollide : MonoBehaviour {


  public GameObject Player;
  public GameObject LightObject;
  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

  }

  void OnCollisionEnter(Collider other)
  {
    if (other.name == Player.name)
    {
      
    }
  }

  void OnCollisionExit(Collider other)
  {
    if (other.name == "Detection Zone")
    {
      Debug.Log("leavingtrigger");
      inSight = false;
      Debug.Log("not in sight");
    }
  }
}
