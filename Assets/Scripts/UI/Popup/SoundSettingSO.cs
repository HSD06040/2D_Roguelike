using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BGMSFXSetting", menuName = "BGMSFXSetting/BGMSFXSetting", order = 0)]
public class SoundSettingSO : ScriptableObject
{
    [Range(0, 1)] public float BGMSetting;
    [Range(0, 1)] public float SFXSetting;
}
