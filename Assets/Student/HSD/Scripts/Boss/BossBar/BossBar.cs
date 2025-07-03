using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    public void SetupMaxValue(float amount) => hpSlider.maxValue = amount;

    public void UpdateSlider(float amount)
    {
        hpSlider.value = hpSlider.maxValue - amount;
    }
}
