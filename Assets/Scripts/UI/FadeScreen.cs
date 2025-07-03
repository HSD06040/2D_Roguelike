using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    float timer;

    public void PlayFade(float _fadeTime, float _delay)
    {
        StartCoroutine(FadeRoutine(_fadeTime, _delay));
    }

    private IEnumerator FadeRoutine(float _fadeTime, float _delay)
    {
        timer = 0f;

        while (timer < _fadeTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(0f, 1f, timer / _fadeTime);
            fadeImage.color = color;
            timer += Time.deltaTime;
            yield return null;
        }

        Color fullVisible = fadeImage.color;
        fullVisible.a = 1f;
        fadeImage.color = fullVisible;

        yield return Utile.GetDelay(_delay);

        timer = 0f;

        while (timer < _fadeTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(1f, 0f, timer / _fadeTime);
            fadeImage.color = color;
            timer += Time.deltaTime;
            yield return null;
        }

        Color fullyTransparent = fadeImage.color;
        fullyTransparent.a = 0f;
        fadeImage.color = fullyTransparent;
    }

}
