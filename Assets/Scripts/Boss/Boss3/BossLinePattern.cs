using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLinePattern : BossPattern_Sound
{
    [SerializeField] private BossPatternObject[] objs;
    [SerializeField] private float delay;

    protected override IEnumerator PatternRoutine()
    {
        base.PatternRoutine();

        yield return Utile.GetDelay(delay);
        
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].Setup(duration, prefab, Vector2.zero, false);

            if(isFirst)
                StartCoroutine(SoundRoutine());

            yield return Utile.GetDelay(interval);
        }

        OnComplated?.Invoke();
    }    
}
