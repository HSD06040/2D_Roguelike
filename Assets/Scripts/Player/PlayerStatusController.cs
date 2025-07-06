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
        
        heartUI.InicialHearts(status.MaxHp.Value);
        status.CurtHp.Value = status.MaxHp.Value;
        status.CurtHp.AddEvent(heartUI.HeartUpdate);
        status.MaxHp.OnChanged += heartUI.InicialHearts;//
        status.OnPlayerDead += Die;
        Manager.Game.OnRetry += PlayerDestroy;
    }

    public override void TakeDamage(float damage)
    {
        Manager.Data.PlayerStatus.DecreaseHealth((int)damage);
    }

    private void Die()
    {
        Manager.Game.IsDead = true;
    }

    private void PlayerDestroy() => Destroy(gameObject);

    private void OnDestroy()
    {
        Manager.Game.OnRetry -= PlayerDestroy;
    }

   //private void Update()
   //{
   //    if (Input.GetKeyDown(KeyCode.P))
   //    {
   //        status.AddStat(StatType.MaxHp, 1, "Test");
   //        Debug.Log($"MaxHp 증가: {status.MaxHp.Value}, 현재 체력: {status.CurtHp.Value}");
   //    }
   //}

}
