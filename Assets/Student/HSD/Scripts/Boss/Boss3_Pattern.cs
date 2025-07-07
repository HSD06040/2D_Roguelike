using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3_Pattern : BossPattern
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
        else
        {
            Manager.Audio.PlaySFX(clip, transform.position);
        }        
    }
}
