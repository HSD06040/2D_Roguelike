using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPatternObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Collider2D col;
    [SerializeField] private Color red;
    [SerializeField] private Color pink;
    [SerializeField] private Color gray;
    [SerializeField] private Color white;
    [SerializeField] private SpriteRenderer parentSr;
    [SerializeField] private SpriteRenderer children;
    private bool isAttack;
    private Vector3 targetScale;

    private void Awake()
    {
        targetScale = target.localScale;
    }

    public void Setup(float _duration, GameObject obj, Vector3 scale, bool isDestroy, bool _isAttack = true)
    {
        gameObject.SetActive(true);

        isAttack = _isAttack;

        if (isAttack)
        {
            parentSr.color = pink;
            children.color = red;
            parentSr.sortingOrder = 0;
            children.sortingOrder = 1;
        }
        else
        {
            parentSr.color = gray;
            children.color = white;
            parentSr.sortingOrder = 2;
            children.sortingOrder = 3;
            target.localScale = Vector2.one;
        }

        if(scale != Vector3.zero)
            transform.localScale = scale;

        StartCoroutine(Routine(_duration, obj, isDestroy)); 
    }

    private IEnumerator Routine(float _duration, GameObject obj, bool isDestroy)
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

        if (isAttack)
            Attack(obj, isDestroy);

        target.localScale = targetScale;

        if (isDestroy)
            Destroy(gameObject, .1f);
        else
            gameObject.SetActive(false);

        col.enabled = false;
    }

    private void Attack(GameObject obj, bool isDestroy)
    {
        Destroy(Instantiate(obj, transform.position, Quaternion.identity),3);
        col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamagable>().TakeDamage(1);
        }
    }
}
