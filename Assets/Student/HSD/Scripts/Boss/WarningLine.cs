using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLine : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;

    public void Init(float _duration)
    {
        StartCoroutine(ColorLessRoutine(_duration));
    }

    private IEnumerator ColorLessRoutine(float duration)
    {
        Color color = sr.color;
        float startAlpha = color.a;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
            sr.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }

        sr.color = new Color(color.r, color.g, color.b, 0f);

        Destroy(gameObject);
    }
}
