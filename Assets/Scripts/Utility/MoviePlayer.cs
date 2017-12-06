﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]

public class MoviePlayer : MonoBehaviour {
  public MovieTexture movie;
  private AudioSource audio;

  public bool transitionIsDone;
  public string nextSceneName;
  AsyncOperation async;
  bool isFading;
  bool isPaused;
  // Use this for initialization
  void Start () {
    GetComponent<RawImage>().texture = movie as MovieTexture;
    audio = GetComponent<AudioSource>();
    audio.clip = movie.audioClip;
    movie.Play();
    audio.Play();
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

  // Update is called once per frame
  void Update () {
		if(!movie.isPlaying && !transitionIsDone)
    {
      StartCoroutine(ChangeSceneHelper(nextSceneName));
      transitionIsDone = true;
    }
	}
}