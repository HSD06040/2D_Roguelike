using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicWeapon : MonoBehaviour
{
    public Weapon WeaponData;
    public int Count;
    private List<GameObject> particles;
    public Transform ParticlePos;

    private GameObject curParticle;
    private GameObject prevParticle;

    public Action<int> OnUpgrade;

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
        particles = new();

        for (int i = 0; i < WeaponData.WeaponParticle.Length; i++)
        {
            particles.Add(WeaponData.WeaponParticle[i]);
        }
    }
    public void Init(Weapon _weaponData)
    {
        WeaponData = _weaponData;
    }


    public GameObject Spawn(Transform playerTransform)
    {
        if (WeaponData.model != null)
        {
            GameObject weapon = Instantiate(WeaponData.model, playerTransform);
            return weapon;
        }
        //animator.runtimeAnimatorController = weaponOverride;

        return null;
    }



    public void SetWeaponNormalParticle(int num)
    {
        curParticle = Instantiate(particles[num], transform.parent);
    }
    
    public void SetWeaponUpgradeParticle(int num)
    {
        Destroy(curParticle);
        curParticle = Instantiate(particles[num], transform.parent);
    }

    //public void SetWeaponParticle(int num)
    //{
    //    GameObject curParticle = null;
    //    if(count < WeaponData.WeaponMaxCount)
    //    {
    //        curParticle = Instantiate(particles[num], transform.parent);
    //    }
    //    else if(count >= WeaponData.WeaponMaxCount)
    //    {
    //        prevParticle = curParticle;
    //        curParticle = Instantiate()
    //    }
    //}
    
}
