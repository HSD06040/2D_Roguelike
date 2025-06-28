using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1 : BossPattern
{
    [SerializeField, Tooltip("������ �����Ǵ� ��ġ ����")]
    private Transform[] patternTransform;

    protected override IEnumerator PatternRoutine()
    {
        for (int i = 0; i < patternTransform.Length; i++)
        {
            Instantiate(prefab, patternTransform[i].position, Quaternion.identity).GetComponent<BossPatternObject>().Setup(duration);
            yield return Utile.GetDelay(interval);
        }
    }
}
