using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

  public int nextScene = 1;

  public void loadNext()
  {
    
    if (nextScene < 0)
      Application.Quit();
        
    SceneManager.LoadScene(nextScene);
    var gObject = GameObject.FindGameObjectWithTag("Click");
    var audio = gObject.GetComponent<AudioSource>();
    audio.Play();
  }

}
