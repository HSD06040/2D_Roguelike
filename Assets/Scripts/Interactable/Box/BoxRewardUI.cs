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
    private Coroutine coroutine;

    public void BoxRewardDisplay(Item item)
    {
        itemIcon.sprite = item.icon;
        itemNameText.text = item.name;
        itemDescription.text = item.description;

        Open();

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        
        coroutine = StartCoroutine(UIAutoCloseCoroutine());

    }

    private IEnumerator UIAutoCloseCoroutine()
    {
        yield return new WaitForSeconds(uiLifeTime);
        Close();
    }

    public override void Open()
    {
 
        background.SetActive(true);
        Animator animator = GetComponent<Animator>();
        animator.Play("In", 0, 0f);
        
    }

    public override void Close()
    {
        base.Close();
        //background.SetActive(false);
    }

}



