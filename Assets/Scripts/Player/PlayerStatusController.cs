using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : StatusController
{
    [SerializeField] private float playerDamageCoolDown;
    [SerializeField] private HealthHeart heartUI;

    public PlayerStatus status;

    private bool hasDamaged = false;

    private new void Start()
    {
        base.Start();

        status = Manager.Data.playerStatus;
        //heartUI.InicialHearts(maxHp); //하트 생성
    }
    public override void TakeDamage(int damage)
    {
        if(!hasDamaged)
        {  
            hasDamaged=true;
            base.TakeDamage(damage);

            heartUI.HeartUpdate(currentHp);

            StartCoroutine(PlayerDamageCoolDown());
        }
    }

    public void Heal(int amount)
    {
        currentHp += amount;
        heartUI.HeartUpdate(currentHp);
    }

    private IEnumerator PlayerDamageCoolDown()  //플레이어 대미지 쿨타임 설정
    {
        yield return new WaitForSeconds(playerDamageCoolDown);
        hasDamaged = false;
    }
}
