using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicZoneControl : MonoBehaviour {

  public AudioMixerSnapshot zone1;
  public AudioMixerSnapshot zone2;
  public float bpm = 128;

  private float m_TransitionIn;
  private float m_TransitionOut;
  private float m_QuarterNote;
  // Use this for initialization
  void Start () {
    m_QuarterNote = 60 / bpm;
    m_TransitionIn = m_QuarterNote;
    m_TransitionOut = m_QuarterNote * 32;
  }


  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Zone"))
    {
      zone2.TransitionTo(m_TransitionIn);
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Zone"))
    {
      zone1.TransitionTo(m_TransitionOut);
    }
  }

  // Update is called once per frame
  void Update () {
		
	}
}
