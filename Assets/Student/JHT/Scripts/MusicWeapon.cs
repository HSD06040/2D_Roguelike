using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicWeapon : MonoBehaviour
{
    public Weapon weaponData;
    public Image weaponImage;
    private int count;
    private GameObject[] particles;
    public Transform ParticlePos;

    public event Action<int> OnUpgrade;

    private void OnEnable()
    {
        OnUpgrade += SetWeaponNormalParticle;
    }

    private void OnDisable()
    {
        OnUpgrade -= SetWeaponNormalParticle;
    }


    private void Start()
    {
        for (int i = 0; i < weaponData.weaponParticle.Length; i++)
        {
            particles[i] = weaponData.weaponParticle[i];
        }
    }
    public void Init(Weapon _weaponData, Transform _particlePos, Sprite weaponSprite)
    {
        weaponData = _weaponData;
        weaponImage.sprite = weaponSprite;
        ParticlePos = _particlePos;
    }


    public GameObject Spawn(Transform playerTransform)
    {
        if (weaponData.model != null)
        {
            GameObject weapon = Instantiate(weaponData.model, playerTransform);
            return weapon;
        }
        //animator.runtimeAnimatorController = weaponOverride;

        return null;
    }

    public void CheckOldWeapon() // 파라미터 Player
    {
        count++;
        if(count > weaponData.weaponMaxCount)
        {
            OnUpgrade?.Invoke(count); //이거 count아님 notSet가져와야함
        }
    }




    public void SetWeaponNormalParticle(int num)
    {
        Instantiate(particles[num], ParticlePos);
    }
    
    public void SetWeaponUpgradeParticle(int num)
    {
        Instantiate(particles[num], ParticlePos);
        Destroy(particles[num - 1]);
    }
    
}
