using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxRewardUI : AnimationUI_Base
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private GameObject background;

    [SerializeField] private float uiLifeTime = 3f;

    private Item item;

    public void BoxRewardDisplay(Item item)
    {
        itemIcon.sprite = item.icon;
        itemNameText.text = item.name;
        itemDescription.text = item.description;

        Open();
        StartCoroutine(UIAutoCloseCoroutine());

    }

    private IEnumerator UIAutoCloseCoroutine()
    {
        yield return new WaitForSeconds(uiLifeTime);
        Close();
    }

    public override void Open()
    {
        //Debug.Log("ui IN");
        base.Open();
        background.SetActive(true);
    }

    public override void Close()
    {
        base.Close();
        background.SetActive(false);
    }

}



