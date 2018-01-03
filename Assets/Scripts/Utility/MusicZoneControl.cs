using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicZoneControl : MonoBehaviour
{

  public AudioMixerSnapshot zone1;
  public AudioMixerSnapshot zone2;
  public float bpm = 128;

  private float m_TransitionIn;
  private float m_TransitionOut;
  private float m_QuarterNote;
  public AudioClip[] SFX;
  public AudioSource SFXSource;
  // Use this for initialization
  void Start () {
    m_QuarterNote = 60 / bpm;
    m_TransitionIn = m_QuarterNote * 10;
    m_TransitionOut = m_QuarterNote * 10;
  }

  void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Vector3 dist = other.transform.position - transform.position;
      if(dist.x > 0)
        zone2.TransitionTo(m_TransitionIn);
      else
        zone1.TransitionTo(m_TransitionIn);
      PlaySFX();
    }
  }

  void PlaySFX()
  {
    int randClip = Random.Range(0, SFX.Length);
    SFXSource.clip = SFX[randClip];
    SFXSource.Play();
  }
}
