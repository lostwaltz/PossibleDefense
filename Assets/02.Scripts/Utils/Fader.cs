using System;
using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;
    private Action onFadeComplete;
    

    public Fader FadeTo(float startAlpha, float targetAlpha, float duration)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeCoroutine(startAlpha, targetAlpha, duration));

        return this;
    }

    public void OnComplete(Action action)
    {
        onFadeComplete -= action;
        onFadeComplete += action;
    }

    public Fader RemoveAction(Action action)
    {
        onFadeComplete -= action;
        
        return this;
    }
    

    private IEnumerator FadeCoroutine(float startAlpha, float targetAlpha,  float duration)
    {
        var time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        onFadeComplete?.Invoke();
        onFadeComplete = null;
    }
}