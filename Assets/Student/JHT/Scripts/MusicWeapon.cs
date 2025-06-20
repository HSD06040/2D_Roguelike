using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicWeapon : MonoBehaviour
{
    public Weapon weaponData;
    public Sprite weaponSprite;
    public Image weaponImage;

    private List<string> weaponList;

    private void Awake()
    {
        weaponSprite = GetComponent<Sprite>();
    }
    private void Start()
    {
        weaponList = new List<string>(6);
        Init(weaponData);
    }

    public void Init(Weapon _weaponData) //Player player
    {
        weaponData = _weaponData;
        //gameObject.trasform = player.transform + 
        SetWeapon(_weaponData); //이전 무기가 있을경우 +particle? or 
    }

    private void SetWeapon(Weapon weaponData)
    {
        if(weaponData == null)
        {
            return;
        }

        if (!weaponList.Contains(weaponData.name)) //(fightManager.count  < weaponData.weaponMaxCount)
        {
            weaponList.Add(weaponData.name);
            weaponData.Spawn(transform);
            SetWeaponNormalParticle();
            //playerPower += weaponPower;
        }
        else //(fightManager.count > weaponData.weaponMaxCount)
        {
            weaponData.CheckOldWeapon();
            SetWeaponUpgradeParticle();
            //playerPower += weaponPlusPower;
            //fightManager.count //어디서든 무기당 count만들고 MaxCount이상이 될 경우
        }
    }


   


    private void SetWeaponNormalParticle()
    {
        weaponImage.sprite = weaponData.icon;
        weaponImage.color = Color.white;
        //weaponData.SetParticle(); 파라미터로 fightManager.Count를 받아와야함
    }

    private void SetWeaponUpgradeParticle()
    {
         //weaponData.DestroyOldParticle();
         weaponImage.sprite = weaponData.icon;
         weaponImage.color = Color.red;
         //weaponData.SetParticle();
    }


}
