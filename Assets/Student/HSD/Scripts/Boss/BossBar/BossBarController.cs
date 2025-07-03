using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossBarController : MonoBehaviour
{
    [SerializeField] private Monster boss;
    [SerializeField] private BossBar bossBar;
    [SerializeField] private string _bossName;
    [SerializeField] private TMP_Text bossName;

    private void OnEnable()
    {
        bossName.text = _bossName;
        bossBar.SetupMaxValue(boss.MaxHealth);
        boss.OnHpChanged += bossBar.UpdateSlider;
        bossBar.UpdateSlider(boss.CurrentHealth);
    }

    private void OnDisable()
    {
        boss.OnHpChanged -= bossBar.UpdateSlider;
    }
}
