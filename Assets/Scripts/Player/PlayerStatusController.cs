using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : StatusController
{
    [SerializeField] private float playerDamageCoolDown;
    [SerializeField] private HealthHeart heartUI;

    public PlayerStatus status;
    public bool invincible;

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
        if (invincible) return;
        Manager.Data.PlayerStatus.DecreaseHealth((int)damage);
        StartCoroutine(InvincibleRoutine(1));
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

    private IEnumerator InvincibleRoutine(float _delay)
    {
        invincible = true;
        yield return Utile.GetDelay(_delay);
        invincible = false;
    }
}
