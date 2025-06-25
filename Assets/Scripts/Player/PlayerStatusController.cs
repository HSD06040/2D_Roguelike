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

        status = Manager.Data.PlayerStatus;
        
        //heartUI.InicialHearts((int)status.MaxHp.Value); //�ִ�ü�¸�ŭ ��Ʈ ����
        //status.CurtHp.Value = (int)status.MaxHp.Value;

        //status.CurtHp.AddEvent(heartUI.HeartUpdate);

    
    }
    public override void TakeDamage(int damage)
    {
        if(!hasDamaged)
        {  
            hasDamaged=true;
            base.TakeDamage(damage);

            status.CurtHp.Value -= damage;

            StartCoroutine(PlayerDamageCoolDown());
        }
    }

    public void Heal(int amount)
    {
        status.CurtHp.Value += amount;
    }

    private IEnumerator PlayerDamageCoolDown()  //�÷��̾� ����� ��Ÿ�� ����
    {
        yield return new WaitForSeconds(playerDamageCoolDown);
        hasDamaged = false;
    }
}
