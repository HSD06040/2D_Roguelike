using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : StatusController
{
    [SerializeField] private float playerDamageCoolDown;
    [SerializeField] private HealthHeart heartUI;

    public PlayerStatus status;

    private new void Start()
    {
        base.Start();

        status = Manager.Data.PlayerStatus;
        
        heartUI.InicialHearts(status.MaxHp.Value); //ÃÖ´ëÃ¼·Â¸¸Å­ ÇÏÆ® »ý¼º
        status.CurtHp.Value = status.MaxHp.Value;

        status.CurtHp.AddEvent(heartUI.HeartUpdate);
    }

    public override void TakeDamage(int damage)
    {

    }

    public void Die()
    {

    }
}
