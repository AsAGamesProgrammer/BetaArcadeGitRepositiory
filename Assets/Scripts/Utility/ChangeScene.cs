using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {


  public bool transitionIsDone;
  public string nextSceneName;
  AsyncOperation async;
  private void OnCollisionEnter(Collision collision)
  {
    if(collision.gameObject.tag == "Player")
    {
      StartCoroutine(ChangeSceneHelper(nextSceneName));
    }
      
  }

  public IEnumerator ChangeSceneHelper(string sceneName)
  {
    Scene oldScene = SceneManager.GetActiveScene();
    
    Camera oldMainCamera = Camera.main;
    
    Camera.main.tag = "Untagged";
    async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    async.allowSceneActivation = false;
    Scene newScene = SceneManager.GetSceneByName(sceneName);
    while(!async.isDone)
    {
      float x = async.progress;
      if(x >=0.9f)
      {
        FindObjectOfType<AudioListener>().enabled = false;
        oldMainCamera.gameObject.SetActive(false);
        foreach (var go in oldScene.GetRootGameObjects())
        {
          go.SetActive(false);
          Destroy(go);
        }
        async.allowSceneActivation = true;
        SceneManager.SetActiveScene(newScene);
        break;
      }
    }
    yield return null;
  }
}