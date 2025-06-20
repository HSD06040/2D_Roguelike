using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    BGM, SFX
}

public class AudioManager : Singleton<AudioManager>
{
    private AudioMixer audioMixer;
    private AudioMixerGroup bgmGroup;
    private AudioMixerGroup sfxGroup;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private Dictionary<string, AudioClip> sfxCached;
    private Dictionary<int, AudioSource> audioSourceCached;

    private Transform audioParent;

    private const string SOUND_PATH = "Sound/";

    public void Init()
    {
        sfxCached = new Dictionary<string, AudioClip>();

        audioMixer = Manager.Resources.Load<AudioMixer>($"{SOUND_PATH}AudioMixer");
        bgmGroup = audioMixer.FindMatchingGroups("BGM")[0];
        sfxGroup = audioMixer.FindMatchingGroups("SFX")[0];
          
        audioParent = new GameObject("Audio").transform;
        GameObject bgm = new GameObject("BGM_Source");
        bgm.transform.parent = audioParent;
        bgmSource = bgm.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.outputAudioMixerGroup = bgmGroup;

        GameObject effect = new GameObject("SFX_Source");
        effect.transform.parent = audioParent;
        sfxSource = effect.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = sfxGroup;
    }

    public void PlaySFX(string name, Vector3 position, float volume = 1, float pitch = 1)
    {
        if (string.IsNullOrEmpty(name)) return;

        if(!sfxCached.TryGetValue(name, out var clip))
            clip = LoadClip(name, SoundType.SFX);                    

        GameObject audioObj = Manager.Resources.Instantiate<GameObject>("SFX_Obj", position, true);

        if(!audioSourceCached.TryGetValue(audioObj.GetInstanceID(), out var audio))
        {
            audio = audioObj.GetComponent<AudioSource>();

            if (audio != null)
                audioSourceCached[audioObj.GetInstanceID()] = audio;
        }

        audio.clip = clip;
        audio.pitch = pitch;
        audio.volume = volume;
        audio.spatialBlend = 1;
        audio.outputAudioMixerGroup = sfxGroup;

        audio.Play();
        Manager.Resources.Destroy(audioObj, clip.length / pitch);
    }

    public void PlaySFX(AudioClip clip, Vector3 position, float volume = 1, float pitch = 1)
    {        
        GameObject audioObj = Manager.Resources.Instantiate<GameObject>("SFX_Obj", position, true);

        if (!audioSourceCached.TryGetValue(audioObj.GetInstanceID(), out var audio))
        {
            audio = audioObj.GetComponent<AudioSource>();

            if (audio != null)
                audioSourceCached[audioObj.GetInstanceID()] = audio;
        }

        audio.clip = clip;
        audio.pitch = pitch;
        audio.volume = volume;
        audio.spatialBlend = 1;
        audio.outputAudioMixerGroup = sfxGroup;

        audio.Play();
        Manager.Resources.Destroy(audioObj, clip.length / pitch);
    }

    public void PlayBGM(string name, float volume = 1, float pitch = 1)
    {
        if (string.IsNullOrEmpty(name)) return;

        bgmSource.Stop();
        bgmSource.clip = LoadClip(name, SoundType.BGM);
        bgmSource.volume = volume;
        bgmSource.pitch = pitch;
        bgmSource.Play();
    }

    private AudioClip LoadClip(string name, SoundType type)
    {
        if (string.IsNullOrEmpty(name)) return null;

        if(type == SoundType.SFX)
        {
            if(sfxCached.ContainsKey(name))
                return sfxCached[name];

            AudioClip clip = Manager.Resources.Load<AudioClip>($"{SOUND_PATH}{type}/{name}");

            if(clip != null)
                sfxCached[name] = clip;

            return clip;
        }
        
        return Manager.Resources.Load<AudioClip>($"{SOUND_PATH}{type}/{name}");
    }

    public void SetVolume(SoundType type, float volume)
    {
        if (type == SoundType.SFX)
            audioMixer.SetFloat("SFX", volume);
        else
            audioMixer.SetFloat("BGM", volume);
    }
}
