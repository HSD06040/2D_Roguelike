using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : StatusController
{
    [SerializeField] private float playerDamageCoolDown;
    private bool hasDamaged = false;

    private new void Start()
    {
        base.Start();
    }
    public override void TakeDamage(int damage)
    {
        if(!hasDamaged)
        {  
            hasDamaged=true;
            base.TakeDamage(damage);

            StartCoroutine(PlayerDamageCoolDown());
        }
    }

    private IEnumerator PlayerDamageCoolDown()  //플레이어 대미지 쿨타임 설정
    {
        yield return new WaitForSeconds(playerDamageCoolDown);
        hasDamaged = false;
    }
}
