using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicWeapon : MonoBehaviour
{
    public Weapon WeaponData;
    public int Count = 0;
    public int Level = 0;
    private List<GameObject> particles;
    protected Transform player;

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


    public void Init(Transform playerTransform) => player = playerTransform;


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

    public virtual void Attack(Vector2 mousePosition) { }
}
