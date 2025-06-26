using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DamagePopupText : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private Vector3 moveOffset;
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve scaleCurve; // 스케일 변화 곡선
    [SerializeField] private AnimationCurve alphaCurve;
    [SerializeField] private Color color;

    private Vector3 startPos;
    private Vector3 endPos;

    public void Init(string text)
    {
        damageText.color = color;
        damageText.text = text;
        startPos = transform.position;
        endPos = startPos + moveOffset;
        StartCoroutine(PopupTextRoutine());
    }

    private IEnumerator PopupTextRoutine()
    {
        float time = 0f;
        Color originalColor = damageText.color;

        while (time < duration)
        {
            float t = time / duration;

            transform.position = Vector3.Lerp(startPos, endPos, t);

            float scale = scaleCurve.Evaluate(t);
            damageText.transform.localScale = Vector3.one * scale;

            float alpha = alphaCurve.Evaluate(t);
            damageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            time += Time.deltaTime;
            yield return null;
        }

        Manager.Pool.PopupRelease(gameObject);
    }
}
