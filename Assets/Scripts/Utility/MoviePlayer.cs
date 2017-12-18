using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource))]

public class MoviePlayer : MonoBehaviour {
  public VideoPlayer movie;
  public bool transitionIsDone;
  public string nextSceneName;
  public Image guiTexture;
  AsyncOperation async;
  bool isFading;
  bool isPaused;
  // Use this for initialization
  void Start () {
    movie.Play();
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
      guiTexture.color = Color.Lerp(guiTexture.color, Color.black, alpha);
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
      guiTexture.color = Color.Lerp(guiTexture.color, Color.black, alpha);
    }
    isFading = false;
  }
  
  // Update is called once per frame
  void Update () {
		if(!movie.isPlaying && !transitionIsDone)
    {
      StartCoroutine(ChangeSceneHelper(nextSceneName));
      transitionIsDone = true;
    }
    if(Input.anyKey&& !transitionIsDone)
    {
      movie.Stop();
      StartCoroutine(ChangeSceneHelper(nextSceneName));
      transitionIsDone = true;
    }
	}
}
