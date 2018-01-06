using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Panda : MonoBehaviour {

    [SerializeField]
    private List<AudioClip> SnoreSFX;

    private bool mSnoreQueued = false;


    private void Update()
    {
        if (!mSnoreQueued)
            StartCoroutine(QueueSnore(Random.Range(3.0f, 5.0f)));
    }

    private IEnumerator QueueSnore(float queueDuration)
    {
        mSnoreQueued = true;

        yield return new WaitForSeconds(queueDuration);

        GetComponent<AudioSource>().PlayOneShot(SnoreSFX[Random.Range(0, SnoreSFX.Count)]);

        mSnoreQueued = false;
    }
}
