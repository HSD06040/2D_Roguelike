using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternObject : MonoBehaviour
{
    [SerializeField] private Transform target;

    public void Setup(float _duration)
    {
        StartCoroutine(Routine(_duration));
    }

    private IEnumerator Routine(float _duration)
    {
        Vector2 start = target.localScale;
        Vector2 end = Vector2.one;

        float elapsed = 0;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _duration);
            target.localScale = Vector2.Lerp(start, end, t);
            yield return null;
        }

        target.localScale = Vector2.one;
        Attack();
    }

    private void Attack()
    {
        // 장판 공격

        Destroy(gameObject);
    }
}
