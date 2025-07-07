using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : StatusController
{
    [SerializeField] private float playerDamageCoolDown;
    [SerializeField] private HealthHeart heartUI;
    [SerializeField] private PostProcessing_Controller postProcessing;
    public PlayerStatus status;
    public bool invincible;

    private new void Start()
    {
        base.Start();

        status = Manager.Data.PlayerStatus;
        
        heartUI.InicialHearts(status.MaxHp.Value);
        status.CurtHp.Value = status.MaxHp.Value;
        status.CurtHp.AddEvent(heartUI.HeartUpdate);
        status.MaxHp.OnChanged += heartUI.InicialHearts;
        status.OnPlayerDead += Die;
        Manager.Game.OnRetry += PlayerDestroy;
    }

    public override void TakeDamage(float damage)
    {
        if (invincible)
        {
            Debug.Log("무적으로 데미지 안받음");
            return;
        }

        if (Manager.Data.PlayerStatus.DecreaseHealth((int)damage))
        {
            if(!Manager.Data.PlayerStatus.Invincible)
                fx.CreatePopupText("회피", Color.yellow);
            else
                fx.CreatePopupText("무적", Color.cyan);
            return;
        }

        fx.CreateTakeDamageMaterial();
        postProcessing.HitScreenRoutine();
        StartCoroutine(InvincibleRoutine(1));
        Manager.Audio.PlaySFX("Player/PlayerDamage", transform.position);
    }

    private void Die()
    {
        Manager.Game.IsDead = true;
        Manager.Audio.PlaySFX("GameOver", transform.position);//////////////
        Manager.Audio.PlayBGM("Ending");
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
