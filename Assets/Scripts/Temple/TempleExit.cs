using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleExit : MonoBehaviour {

    [SerializeField]
    private Transform ExitTransform;
    [SerializeField]
    private float CollapseSpeed = 8f;
    [SerializeField]
    private AudioClip SFX;
    [SerializeField]
    private AudioSource Source;

    private bool mOpenExit = false;

	void Update ()
    {
        if(mOpenExit)
            ExitTransform.Translate(Vector3.down * Time.deltaTime * CollapseSpeed);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (mOpenExit)
                return;
            Source.PlayOneShot(SFX);
            mOpenExit = true;
        }
    }
}
