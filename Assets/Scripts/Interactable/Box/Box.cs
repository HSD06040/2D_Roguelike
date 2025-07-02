using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Progress;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interectionUI;
    [SerializeField] private BoxRewardUI boxUIScript;

    private Animator animator;
    private bool hasOpended = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        interectionUI.SetActive(false);
    }

    private void Start()
    {
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
        int random = Random.Range(0, 5);
        Item rewardItem = Manager.Data.MusicWeapons[random].WeaponData;

        boxUIScript.BoxRewardDisplay(rewardItem);
        Manager.Data.PlayerStatus.AddItem(rewardItem);
        Debug.Log("아이템 지급 :" + rewardItem.name);
    }
}
