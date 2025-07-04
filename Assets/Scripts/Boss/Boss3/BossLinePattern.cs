using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLinePattern : BossPattern
{
    [SerializeField] private BossPatternObject[] objs;
    [SerializeField] private float delay;

    protected override IEnumerator PatternRoutine()
    {
        yield return Utile.GetDelay(delay);

        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].Setup(duration, prefab, Vector2.zero, false);
            yield return Utile.GetDelay(interval);
        }

        for (int i = objs.Length -2; i >= 0; i--)
        {
            objs[i].Setup(duration, prefab, Vector2.zero, false);
            yield return Utile.GetDelay(interval);
        }

        OnComplated?.Invoke();
    }    
}
