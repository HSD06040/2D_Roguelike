using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText;

    private GoldPresenter goldPresenter;

    private void Awake()
    {
        goldPresenter = new GoldPresenter(this);
    }

    private void OnEnable()
    {
        goldPresenter.AddEvent();
        UpdateGold(Manager.Data.Gold.Value);
    }

    private void OnDisable()
    {
        goldPresenter.RemoveEvent();
    }

    public void UpdateGold(int amount)
    {
        goldText.text = amount.ToString("N0");
    }
}
