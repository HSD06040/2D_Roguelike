using System.Collections;
using System.Collections.Generic;
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

    public void Setup(float _duration, GameObject obj, Vector3 scale, bool _isAttack = true)
    {        
        if(_isAttack)
        {
            parentSr.color = pink;
            children.color = red;
        }
        else

        {
            parentSr.color = gray;
            children.color = white;
        }

        isAttack = _isAttack;
        transform.localScale = scale;
        StartCoroutine(Routine(_duration, obj));
        col.enabled = false;
    }

    private IEnumerator Routine(float _duration, GameObject obj)
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

        if(isAttack)
            Attack(obj);
    }

    private void Attack(GameObject obj)
    {
        Instantiate(obj,transform.position, Quaternion.identity);
        col.enabled = true;

        Destroy(gameObject, .1f);
    }
}
