using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicWeapon : MonoBehaviour
{
    //[SerializeField] private GameObject gunPrefab = null;
    //[SerializeField] private GameObject trumpetPrefab = null;

    public Weapon WeaponData;
    public int Count = 0;
    public int Level = 0;
    private List<GameObject> particles;

    private GameObject curParticle;
    private GameObject prevParticle;

    public Action<int> OnUpgrade;

    private void OnEnable()
    {
        OnUpgrade += SetWeaponParticle;
    }

    private void OnDisable()
    {
        OnUpgrade -= SetWeaponParticle;
    }


    private void Start()
    {
        particles = new();

        for (int i = 0; i < WeaponData.WeaponParticle.Length; i++)
        {
            particles.Add(WeaponData.WeaponParticle[i]);
        }
    }

    //public GameObject Spawn(Transform playerTransform)
    //{
    //    //switch(type)
    //    //{
    //    //    case MusicWeaponType.Gun:
    //    //        return Instantiate(gunPrefab, playerTransform).GetComponent<MusicWeapon>();
    //    //    case MusicWeaponType.Trumpet:
    //    //        return Instantiate(trumpetPrefab, playerTransform).GetComponent<MusicWeapon>();
    //    //    default:
    //    //        return null;
    //    //}
    //
    //    if (WeaponData.model != null)
    //    {
    //        GameObject weapon = Instantiate(WeaponData.model, playerTransform);
    //        return weapon;
    //    }
    //    else
    //    {
    //        Debug.Log("MusicWeapon Spawn : null");
    //    }
    //    //animator.runtimeAnimatorController = weaponOverride;
    //
    //    return null;
    //}


    public void SetWeaponParticle(int num)
    {
        if (Level >= 4) return;
        if (curParticle != null)
        {
            Destroy(curParticle);
        }
        curParticle = Instantiate(particles[num-1], transform.parent);
    }
    
    public void SetWeaponUpgradeParticle(int num)
    {
        Destroy(curParticle);
        curParticle = Instantiate(particles[num], transform.parent);
    }

    
    //플레이어에서 curWeapon에 대한 정보를 기반으로 오기때문에 따로 설정안해도됨
    //Projectile을 상속받은 하위Projectile에서 해당 오브젝트의 특징을 만들어주자
    public virtual void Attack(Vector3 mousePosition) { }
    
}
