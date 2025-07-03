using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingPopUp : BaseUI
{
    public SoundSettingSO SoundSettingData;
    private TextMeshProUGUI returnText => GetUI<TextMeshProUGUI>("ReturnButtonText");

    private Slider setBGM => GetUI<Slider>("BGMSlider");
    private Slider setSFX => GetUI<Slider>("SFXSlider");

    private TextMeshProUGUI setValueOfBGMText => GetUI<TextMeshProUGUI>("BGMValueText");
    private TextMeshProUGUI setValueOfSFXText => GetUI<TextMeshProUGUI>("SFXValueText");

    void Start()
    {
        
        setBGM.value = SoundSettingData.BGMSetting;
        setSFX.value = SoundSettingData.SFXSetting;

        setValueOfBGMText.text = Mathf.RoundToInt(setBGM.value * 100).ToString();
        setValueOfSFXText.text = Mathf.RoundToInt(setSFX.value * 100).ToString();
        
        GetEvent("ReturnButtonText").Click += data =>
        { 
            Manager.UI.ClosePopUp();
        };
        GetEvent("ReturnButtonText").Enter += data => { returnText.color = Color.cyan; };
        GetEvent("ReturnButtonText").Exit += data => { returnText.color = Color.black; };

        setBGM.onValueChanged.AddListener(ChangeBGM);
        setSFX.onValueChanged.AddListener(ChangeSFX);

    }

    private void OnDestroy()
    {
        setBGM.onValueChanged.RemoveListener(ChangeBGM);
        setSFX.onValueChanged.RemoveListener(ChangeSFX);
    }


    private void ChangeBGM(float _setBGMValue)
    {
        SoundSettingData.BGMSetting = _setBGMValue;
        float value = Mathf.Log10(Mathf.Max(SoundSettingData.BGMSetting, 0.0001f)) * 20;
        Manager.Audio.SetVolume(SoundType.BGM, value);
        setValueOfBGMText.text = Mathf.RoundToInt(SoundSettingData.BGMSetting * 100).ToString();
    }
    private void ChangeSFX(float _setSFXValue)
    {
        SoundSettingData.SFXSetting = _setSFXValue;
        float value = Mathf.Log10(Mathf.Max(SoundSettingData.SFXSetting, 0.0001f)) * 20;
        Manager.Audio.SetVolume(SoundType.SFX, value);
        setValueOfSFXText.text = Mathf.RoundToInt(SoundSettingData.SFXSetting * 100).ToString();
    }

}
