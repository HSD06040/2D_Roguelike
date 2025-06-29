using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2 : BossPattern
{
    [SerializeField] private Transform[] points;

    protected override IEnumerator PatternRoutine(Monster boss)
    {
        for (int i = 0; i < points.Length; i++)
        {
            Instantiate(prefab, points[i].position, Quaternion.identity).GetComponent<BossPatternObject>().Setup(duration);
            yield return Utile.GetDelay(interval);
        }
    }
}
