using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interectionUI;
    [SerializeField] private BoxRewardUI boxUIScript;
    [SerializeField] private Item[] boxItems;

    private Animator animator;
    private bool hasOpended = false;
    private Item boxRewardItem;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        interectionUI.SetActive(false);
    }

    public void Interact()
    {
        if (!hasOpended)
        {
            hasOpended = true;
            animator.SetTrigger("Open");
            Debug.Log("상자열림");
            UiOff();

            RamdomBoxRewardItem();
            
        }
    }

    public void UiOn()
    {
        if (!hasOpended)
        {
            interectionUI.SetActive(true);
        }
    }

    public void UiOff()
    {
        interectionUI.SetActive(false);
    }

    private void RamdomBoxRewardItem()
    {
        int random = Random.Range(0, boxItems.Length);
        boxRewardItem = boxItems[random];
        boxUIScript.BoxRewardDisplay(boxRewardItem);
        //플레이어한테 아이템 전달
    }
}
