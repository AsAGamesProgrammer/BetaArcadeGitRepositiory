using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {


  public bool transitionIsDone;
  public string nextSceneName;
  AsyncOperation async;
  bool isFading;
  bool isPaused;

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
    yield return Resources.UnloadUnusedAssets();
    AsyncOperation AO = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    AO.allowSceneActivation = false;
    while (AO.progress < 0.9f)
    {
      yield return null;
    }
    BeginFadeIn(0.5f);
    while (isFading)
      yield return new WaitForEndOfFrame();
    AO.allowSceneActivation = true;
    BeginFadeOut(0.5f);
    while (isFading)
      yield return new WaitForEndOfFrame();
    yield return null;
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
      yield return new WaitForEndOfFrame();
      if (!isPaused)
        alpha = Mathf.Clamp01(alpha + Time.deltaTime / duration);
      DrawQuad(Color.black, alpha);
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
      yield return new WaitForEndOfFrame();
      if (!isPaused)
        alpha = Mathf.Clamp01(alpha - Time.deltaTime / duration);
      DrawQuad(Color.black, alpha);
    }
    isFading = false;
  }
  private static void DrawQuad(Color color, float alpha)
  {
    color.a = alpha;
    Texture2D fadeTexture = new Texture2D(Screen.width, Screen.height);
    int drawDepth = -1000;
    GUI.color = color;
    GUI.depth = drawDepth;
    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
  }
}