using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Lion : MonoBehaviour {

    [SerializeField]
    private AudioClip AnvilSFX;


	public void AnvilHit()
    {
        GetComponent<AudioSource>().PlayOneShot(AnvilSFX, Random.Range(0.3f, 0.6f));
    }
}
