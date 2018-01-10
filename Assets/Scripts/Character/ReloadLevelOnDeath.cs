using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevelOnDeath : MonoBehaviour {
    
	void Update () {
        if (FindObjectOfType<Player>().IsDoneDying)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
