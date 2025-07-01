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
        timer = 0;
        while (timer < _fadeTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(0, 1, timer / _fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return Utile.GetDelay(_delay);

        timer = 0;
        while (timer < _fadeTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(1, 0, timer / _fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
