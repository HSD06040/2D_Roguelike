using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicWeapon", menuName = "MusicWeapon/Basic", order = 0)]
public class Weapon : Item
{
    //public List<string> skills;
    public int power;
    public int plusPower;
    public AnimatorOverrideController weaponOverride = null;
    public float attackRange;
    public float attackDelay;
    public float attackRadius;
    public int weaponMaxCount;
    public GameObject[] weaponParticle;
    public GameObject curParticle;

    //스폰되는 위치인데 필요시 Transform을 Player로 바꾸고 Player의 위치를
    //Queue<Vector3>로 받아와서 Transform사용하면 됨
    public void Spawn(Transform playerTransform)
    {
        CheckOldWeapon();
        if (model != null)
        {
            GameObject weapon = Instantiate(model, playerTransform);
        }
    }

    public void CheckOldWeapon()
    {
        //fightManager.Count++;
    }

    public void SetParticle(int num) // playerTransform
    {
        if(num == 0)
        {
            curParticle = Instantiate(weaponParticle[0]); // playerTransform
        }
        else if(num == 1)
        {
            curParticle = Instantiate(weaponParticle[1]); // playerTransform
        }
    }

    public float GetDamage()
    {
        return power;
    }

    public float GetRange()
    {
        return attackRange;
    }

    public float GetRadius()
    {
        return attackRadius;
    }

    public float GetDelay()
    {
        return attackDelay;
    }
}
