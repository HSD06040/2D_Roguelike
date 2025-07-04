using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : BossPattern
{
    [Header("Transforms")]
    [SerializeField] private Transform center;
    [SerializeField] private Transform[] sides;
            
    private Vector3 startPos;
    private Vector3 targetPos;
    private Transform targetTransform;
    private float timer;
    [SerializeField] private bool isCenter;

    protected override IEnumerator PatternRoutine()
    {
        startPos = boss.transform.position;
        targetPos = boss.transform.position + new Vector3(0,10,0);
        timer = 0;

        while (timer < duration)
        {
            float t = Mathf.SmoothStep(0, 1, timer / duration);            
            boss.transform.position = Vector3.Lerp(startPos, targetPos, t);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0;

        if(isCenter)
        {
            startPos = center.transform.position + new Vector3(0, 10, 0);
            targetPos = center.transform.position;
        }
        else
        {
            targetTransform = sides[Random.Range(0, sides.Length)];
            startPos = targetTransform.position + new Vector3(0, 10, 0);
            targetPos = targetTransform.position;
        }

        yield return Utile.GetDelay(interval);

        while (timer < duration)
        {
            float t = Mathf.SmoothStep(0, 1, timer / duration);
            boss.transform.position = Vector3.Lerp(startPos, targetPos, t);
            timer += Time.deltaTime;
            yield return null;
        }
        boss.transform.position = targetPos;

        OnComplated?.Invoke();
    }
}
