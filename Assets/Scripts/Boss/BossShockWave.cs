using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShockWave : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float duration;
    private float timer;

    private void Start()
    {
        StartCoroutine(ShockWaveRoutine());
    }

    private IEnumerator ShockWaveRoutine()
    {
        while (timer < duration)
        {
            float t = Mathf.SmoothStep(0, 1 ,timer / duration);
            sr.material.SetFloat("_WaveDistanceFromCenter", Mathf.Lerp(0, 1, t));
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
