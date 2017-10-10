using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollide : MonoBehaviour {


  public GameObject Player;
  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

  }

  void OnTriggerEnter(Collider other)
  {
    if (other.name == Player.name)
    {
      var targetScript = GetComponent<LightObject> ();
      if (targetScript == null)
        return;
      if(!targetScript.LightOn)
        targetScript.LightOn = true;
    }
  }
}
