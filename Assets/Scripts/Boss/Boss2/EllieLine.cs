using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllieLine : MonoBehaviour
{
    private float timer;
    private float duration = .3f;
    private float deleteDelay = 12f;

    public void Init(float _deleteDelay, float _duration)
    {
        duration = _duration;
        deleteDelay = _deleteDelay;
    }

    private void Start()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        Vector3 start = transform.localScale;
        Vector3 end = transform.localScale + new Vector3(0, 13, 0);

        timer = 0;
        while (timer < duration)
        {
            transform.localScale = Vector3.Lerp(start, end, Mathf.Clamp01(timer / duration));
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = end;

        yield return Utile.GetDelay(deleteDelay);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << 7 & (1 << collision.gameObject.layer)) != 0)
        {
            collision.GetComponent<IDamagable>().TakeDamage(1);
        }
    }
}
