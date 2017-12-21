using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCredits : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Player>())
            SceneManager.LoadScene(5);
    }
}
