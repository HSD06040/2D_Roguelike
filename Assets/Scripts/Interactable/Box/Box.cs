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

    //private void RamdomBoxRewardItem()  //무기확률 50%
    //{
    //    List<Item> randomList = new();
    //
    //    int weaponRandom = Random.Range(0, Manager.Data.MusicWeapons.Length);
    //    Item randomWeapon = Manager.Data.MusicWeapons[weaponRandom].WeaponData;
    //    randomList.Add(randomWeapon);
    //
    //    Item randomItem = TableManager.GetInstance().GetRandomItem();
    //    randomList.Add(randomItem);
    //
    //    int finalReward = Random.Range(0, randomList.Count);
    //    Item rewardItem = randomList[finalReward];
    //
    //    boxUIScript.BoxRewardDisplay(rewardItem);
    //    Manager.Data.PlayerStatus.AddItem(rewardItem);
    //    Debug.Log("아이템 지급 :" + rewardItem.name);
    //}

    private void RamdomBoxRewardItem()
    {
        List<Item> randomList = new();

        foreach(var weapon in Manager.Data.MusicWeapons)
        {
            randomList.Add(weapon.WeaponData);
        }

        randomList.AddRange(Manager.Table.GetAllItems());

        Item rewardItem = randomList[Random.Range(0, randomList.Count)];

        Manager.UI.BoxReward.BoxRewardDisplay(rewardItem);
        Manager.Data.PlayerStatus.AddItem(rewardItem);
        Debug.Log("아이템 지급 :" + rewardItem.name);
    }
}
