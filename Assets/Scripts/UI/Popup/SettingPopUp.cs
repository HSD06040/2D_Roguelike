using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopUp : BaseUI
{
    private TextMeshProUGUI returnText => GetUI<TextMeshProUGUI>("ReturnButtonText");

    private Slider setBGM => GetUI<Slider>("BGMSlider");
    private Slider setSFX => GetUI<Slider>("SFXSlider");

    private TextMeshProUGUI setValueOfBGMText => GetUI<TextMeshProUGUI>("BGMValueText");
    private TextMeshProUGUI setValueOfSFXText => GetUI<TextMeshProUGUI>("SFXValueText");


    private event Action<float> OnChangeBGM;
    private event Action<float> OnChangeSFX;

    private void OnEnable()
    {
        OnChangeBGM += ChangeBGM;
        OnChangeBGM += ChangeSFX;
    }

    private void OnDisable()
    {
        OnChangeBGM -= ChangeBGM;
        OnChangeBGM -= ChangeSFX;
    }
    void Start()
    {
        GetEvent("ReturnButtonText").Click += data => { Manager.UI.ClosePopUp(); };
        GetEvent("ReturnButtonText").Enter += data => { returnText.color = Color.cyan; };
        GetEvent("ReturnButtonText").Exit += data => { returnText.color = Color.black; };
    }

    private void Update()
    {
        if(setBGM != null)
        {
            OnChangeBGM?.Invoke(Mathf.Log10(setBGM.value) * 20);
        }

        if(setSFX != null)
        {
            OnChangeSFX?.Invoke(Mathf.Log10(setSFX.value) * 20);
        }
    }

    private void ChangeBGM(float _setBGMValue)
    {
        Manager.Audio.SetVolume(SoundType.BGM, _setBGMValue);
        setValueOfBGMText.text = _setBGMValue.ToString();
    }
    private void ChangeSFX(float _setSFXValue)
    {
        Manager.Audio.SetVolume(SoundType.SFX, _setSFXValue);
        setValueOfSFXText.text = _setSFXValue.ToString();
    }

}
