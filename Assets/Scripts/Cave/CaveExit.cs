using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveExit : MonoBehaviour {

    [SerializeField]
    private ButtonPuzzleManager PuzzleManager;
    [SerializeField]
    private float CollapseSpeed = 5f;
    [SerializeField]
    private AudioClip SFX;

    bool mCanSound = true;

    private void Update()
    {
        if (PuzzleManager.PuzzleComplete)
        {
            transform.Translate(Vector3.down * Time.deltaTime * CollapseSpeed, Space.World);
            if(mCanSound)
            {
                GetComponent<AudioSource>().PlayOneShot(SFX);
                mCanSound = false;
            }
        }
    }
}
