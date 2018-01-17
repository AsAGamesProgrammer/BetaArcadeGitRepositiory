using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {


  public bool transitionIsDone;
  public string nextSceneName;
  AsyncOperation async;
  bool isFading;
  bool isPaused;
  public new Image guiTexture;

  public void Start()
  {
    BeginFadeOut(1.5f);
  }

  private void OnCollisionEnter(Collision collision)
  {
    if(collision.gameObject.tag == "Player")
    {
      
      StartCoroutine(ChangeSceneHelper(nextSceneName));
    }    
  }

  public IEnumerator ChangeSceneHelper(string sceneName)
  {
    BeginFadeIn(1.5f);
    while (isFading)
    {
      yield return null;
    }
    SceneManager.LoadScene(sceneName);
    yield return null;

    //Scene oldScene = SceneManager.GetActiveScene();
    //Camera oldMainCamera = Camera.main;
    ////if(Camera.main != null)
    ////    Camera.main.tag = "Untagged";
    //yield return Resources.UnloadUnusedAssets();
    //AsyncOperation AO = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    //AO.allowSceneActivation = false;
    
    //BeginFadeIn(0.5f);
    //while (isFading)
    //  yield return new WaitForEndOfFrame();
    //AO.allowSceneActivation = true;
    //BeginFadeOut(0.5f);
    //while (isFading)
    //  yield return new WaitForEndOfFrame();
    //yield return null;
  }

  public void BeginFadeIn(float duration)
  {
    if (isFading)
      return;
    isFading = true;
    StartCoroutine(FadeInAsync(duration, Color.black));
  }
  private IEnumerator FadeInAsync(float duration, Color color)
  {
    var alpha = 0.0f;
    while (alpha < 1.0f)
    {
      alpha = Mathf.Clamp01(alpha + Time.deltaTime / duration);
      guiTexture.color = Color.Lerp(guiTexture.color, Color.black, alpha);
      yield return new WaitForEndOfFrame();
    }
    isFading = false;
  }

  public void BeginFadeOut(float duration)
  {
    if (isFading)
      return;
    isFading = true;
    StartCoroutine(FadeOutAsync(duration, Color.black));
  }
  private IEnumerator FadeOutAsync(float duration, Color color)
  {
    var alpha = 1.0f;
    while (alpha > 0.0f)
    {
      alpha = Mathf.Clamp01(alpha - Time.deltaTime / duration);
      guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, alpha);
      yield return new WaitForEndOfFrame();
    }
    isFading = false;
  }
  //private static void DrawQuad(Color color, float alpha)
  //{
  //  color.a = alpha;
  //  Texture2D fadeTexture = new Texture2D(Screen.width, Screen.height);
  //  int drawDepth = -1000;
  //  GUI.color = color;
  //  GUI.depth = drawDepth;
  //  GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
  //}
}