using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{

  public int TimeOfLight;
  public float Intensity;
  public bool LightOn;
  public float LightRange;
  float TimeOfRun;
  bool IsRunning = false;

    // Use this for initialization
  void Start ()
  {
    Light objectLight = GetComponent<Light>();
    if (objectLight != null && LightOn)
    {
      objectLight.enabled = LightOn;
      objectLight.intensity = Intensity;
      objectLight.range = LightRange;
    }
  }


  private IEnumerator TimerCount( Light objectLight)
  {
    IsRunning = true;
    while (TimeOfRun > 0)
    {
      if(objectLight.range > 0)
        objectLight.range -= 0.1f;
      yield return new WaitForSeconds(0.1f);
    }
    if(TimeOfRun >= 0)
    {
      objectLight.enabled = false;
      objectLight.intensity = Intensity;
      TimeOfRun = TimeOfLight;
    }
    IsRunning = false;
  }
  // Update is called once per frame
  void Update ()
  {
    Light objectLight = GetComponent<Light>();
    if (objectLight == null)
      return;
    if (LightOn)
    {
      objectLight.enabled = LightOn;
      objectLight.intensity = Intensity;
      objectLight.range = LightRange;
      TimeOfRun = TimeOfLight;
      LightOn = false;
    }
    if (!IsRunning)
    {
      StartCoroutine(TimerCount(objectLight));

    }


  }
}
