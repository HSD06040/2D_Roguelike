using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllieLine : MonoBehaviour
{
    private float timer;

    private void Start()
    {
        StartCoroutine(Routine(.3f));
    }

    private IEnumerator Routine(float _duration)
    {
        Vector3 start = transform.localScale;
        Vector3 end = transform.localScale + new Vector3(0, 13, 0);

        timer = 0;
        while (timer < _duration)
        {
            transform.localScale = Vector3.Lerp(start, end, Mathf.Clamp01(timer / _duration));
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = end;

        yield return Utile.GetDelay(12);

        start = transform.localScale;
        end = new Vector3(0, transform.localScale.y, transform.localScale.z);

        timer = 0;
        while(timer < .4f)
        {
            transform.localScale = Vector3.Lerp(start, end, Mathf.Clamp01(timer / .4f));
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
