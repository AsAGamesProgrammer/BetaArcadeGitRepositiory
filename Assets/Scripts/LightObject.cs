using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour {

  public float TimeOfLight;
  public float Intensity;
  public bool LightOn;
  float TimeOfRun;

	// Use this for initialization
	void Start () {
    Light objectLight = GetComponent<Light>();
    if (objectLight != null)
    {
      objectLight.enabled = LightOn;
      objectLight.intensity = Intensity;
      TimeOfRun = TimeOfLight;
    }
  }
	
	// Update is called once per frame
	void Update () {
    Light objectLight = GetComponent<Light>();
  }
}
