using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlissandoPattern : BossPattern
{
    [SerializeField] private BossPatternObject[] objs;
    [SerializeField] private float delay;
    private int random;

    protected override IEnumerator PatternRoutine()
    {
        random = Random.Range(6, objs.Length - 6);

        for (int i = 0; i < objs.Length; i++)
        {
            if (i == random)
                objs[i].Setup(duration, prefab, Vector3.zero, false, false);
            else
                objs[i].Setup(duration, prefab, Vector3.zero, false);

            yield return Utile.GetDelay(interval);
        }

        yield return Utile.GetDelay(delay);

        random = Random.Range(6, objs.Length - 6);

        for (int i = objs.Length -1; i >= 0; i--)
        {
            if (i == random)
                objs[i].Setup(duration, prefab, Vector3.zero, false, false);
            else
                objs[i].Setup(duration, prefab, Vector3.zero, false);

            yield return Utile.GetDelay(interval);
        }

        yield return Utile.GetDelay(delay);

        OnComplated?.Invoke();
    }
}
