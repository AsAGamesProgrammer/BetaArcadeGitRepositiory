using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Restart level on trigger enter
/// </summary>

public class OnTriggerRestart : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Colliding");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
