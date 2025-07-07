using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern_Sound : BossPattern
{
    protected bool isFirst;
    [SerializeField] private AudioClip clip;

    protected override IEnumerator PatternRoutine()
    {
        isFirst = true;
        yield return null;
    }

    protected IEnumerator SoundRoutine(AudioClip audio = null)
    {
        isFirst = false;
        yield return Utile.GetDelay(duration);

        if(audio != null)
        {
            Manager.Audio.PlaySFX(audio, transform.position);
        }   
        else if(clip != null)
        {
            Manager.Audio.PlaySFX(clip, transform.position);
        }        
    }
}
