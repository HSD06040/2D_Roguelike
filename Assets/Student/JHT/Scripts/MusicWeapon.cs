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
    public int Count;
    public int Level;
    private List<GameObject> particles;

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


    public void SetWeaponNormalParticle(int num)
    {
        curParticle = Instantiate(particles[num], transform.parent);
    }
    
    public void SetWeaponUpgradeParticle(int num)
    {
        Destroy(curParticle);
        curParticle = Instantiate(particles[num], transform.parent);
    }

    
    //�÷��̾�� curWeapon�� ���� ������ ������� ���⶧���� ���� �������ص���
    //Projectile�� ��ӹ��� ����Projectile���� �ش� ������Ʈ�� Ư¡�� ���������
    public virtual void Attack(Vector3 mousePosition) { }
    
}
