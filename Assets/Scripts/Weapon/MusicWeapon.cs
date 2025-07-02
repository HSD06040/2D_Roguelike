using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicWeapon : MonoBehaviour
{
    public Weapon WeaponData;
    public int Level = 0;
    private List<GameObject> particles;
    protected Transform player;
    public int curAttackDamage;

    private GameObject curParticle;
    private GameObject prevParticle;

    public Action<int> OnUpgrade;

    private void OnEnable()
    {
        OnUpgrade += SetUpgradeWeapon;
    }

    private void OnDisable()
    {
        OnUpgrade -= SetUpgradeWeapon;
    }


    private void Start()
    {
        particles = new();
        //curAttackDamage = WeaponData.AttackDamage[0];
        for (int i = 0; i < WeaponData.WeaponParticle.Length; i++)
        {
            particles.Add(WeaponData.WeaponParticle[i]);
        }
    }


    public void Init(Transform playerTransform) => player = playerTransform;


    public void SetUpgradeWeapon(int num)
    {
        if (Level >= 5) return;
        if (curParticle != null)
        {
            Destroy(curParticle);
        }
        curParticle = Instantiate(particles[num-1], transform.parent);
        curAttackDamage = WeaponData.AttackDamage[num];
    }

    public virtual void Attack(Vector2 mousePosition) { }
}
