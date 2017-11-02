using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {


  public bool transitionIsDone;
  public string nextSceneName;
  AsyncOperation async;
  void Start () {
  }

  public IEnumerator ChangeSceneHelper(string sceneName)
  {

    Scene oldScene = SceneManager.GetActiveScene();
    
    Camera oldMainCamera = Camera.main;
    
    Camera.main.tag = "Untagged";
    async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    Scene newScene = SceneManager.GetSceneByName(sceneName);
    while (!async.isDone)
    {
      float x = async.progress;
      if (async.progress >= 0.9f)
      {
        FindObjectOfType<AudioListener>().enabled = false;
        oldMainCamera.gameObject.SetActive(false);
        foreach (var go in oldScene.GetRootGameObjects())
        {
          go.SetActive(false);
          Destroy(go);
        }
        transitionIsDone = false;
        SceneManager.SetActiveScene(newScene);
        yield return null;
      }
    }
  }

  void Update () {
    if (transitionIsDone)
    {
      StartCoroutine(ChangeSceneHelper(nextSceneName));
    } 
  }
}
