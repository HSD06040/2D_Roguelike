using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PianoPatternType { WhiteAttak, BlackAttack }

public class PianoPattern : BossPattern
{
    [SerializeField] private PianoPatternType type;
    [SerializeField] private BossPatternObject[] white;
    [SerializeField] private BossPatternObject[] black;
    private static readonly Vector3 blackScale = new Vector3(0.9431068f, 2.204891f, 1);
    private static readonly Vector3 whiteScale = new Vector3(1.235714f, 4.5f, 1);

    protected override IEnumerator PatternRoutine()
    {
        if(type == PianoPatternType.WhiteAttak)
        {
            for(int i = 0; i < white.Length; i ++)
            {
                white[i].Setup(duration, prefab, whiteScale, false);
            }
            for(int i = 0; i < black.Length; i ++)
            {
                black[i].Setup(duration, prefab, blackScale, false, false);
            }
        }
        else
        {
            for (int i = 0; i < white.Length; i++)
            {
                white[i].Setup(duration, prefab, whiteScale, false, false);
            }
            for (int i = 0; i < black.Length; i++)
            {
                black[i].Setup(duration, prefab, blackScale, false);
            }
        }

        yield return null;
        OnComplated?.Invoke();
    }
}
