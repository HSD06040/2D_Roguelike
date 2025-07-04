using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[RequireComponent(typeof(EntityFX))]
public class Monster : MonoBehaviour, IDamagable
{
    [Header("피격 효과 설정")]
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float hitEffectDuration = 0.15f;

    [field: SerializeField] public float MaxHealth { get; protected set; } = 100f;
    [field: SerializeField] public int AttackPower { get; protected set; } = 10;
    private float curHP;
    public float CurrentHealth { get { return curHP; } protected set { curHP = value; OnHpChanged?.Invoke(curHP); } }
    public EntityFX fx { get; protected set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public int FacingDirection;

    public SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Material originalMaterial;
    private bool isHitCoroutineRunning = false;
    public bool Invincible;
    public Action OnDied;
    public Action<float> OnHpChanged;

    public virtual void SetStats(float maxHealth, int attackPower)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        AttackPower = attackPower;
    }

    protected virtual void Awake()
    {
        CurrentHealth = MaxHealth;
        Animator = GetComponentInChildren<Animator>();
        fx = GetComponent<EntityFX>();
        Rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
            originalMaterial = spriteRenderer.material;
        }
    }

    public virtual void Flip(Transform target)
    {
        if (target == null) return;

        float xPosition = target.position.x;
        int direction = xPosition > transform.position.x ? 1 : -1;

        if (FacingDirection != direction)
        {
            FacingDirection = direction;
            transform.localScale = new Vector3(FacingDirection, 1, 1);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (Invincible) return;

        CurrentHealth -= damage;

        if (CurrentHealth > 0)
        {
            if (!isHitCoroutineRunning)
            {
                StartCoroutine(HitFlashCoroutine());
            }
        }
        else
        {
            OnDied?.Invoke();
        }
        fx.CreatePopupText(damage);
    }

    private IEnumerator HitFlashCoroutine()
    {
        isHitCoroutineRunning = true;

        if (whiteFlashMaterial != null)
        {
            spriteRenderer.material = whiteFlashMaterial;
        }
        yield return new WaitForSeconds(hitEffectDuration);

        spriteRenderer.material = originalMaterial;
        spriteRenderer.color = originalColor;

        isHitCoroutineRunning = false;
    }

    public void DropCoin(MonsterStat monsterStat)
    {
        Debug.Log("DropCoin 메소드를 호출");
        if (monsterStat.CoinPrefab != null && monsterStat.GetCoinAmount > 0)
        {
            GameObject coinObject = Instantiate(
                monsterStat.CoinPrefab,
                transform.position,
                Quaternion.identity
            );

            Coin coinController = coinObject.GetComponent<Coin>();

            if (coinController != null)
            {
                coinController.Init(monsterStat.GetCoinAmount);
            }

            if (Manager.Data.GoldStat.HitGoldChance.Value > UnityEngine.Random.Range(0, 100))
            {
                coinObject = Instantiate(
                monsterStat.CoinPrefab,
                transform.position,
                Quaternion.identity
                );

                coinController = coinObject.GetComponent<Coin>();
                if (coinController != null)
                {
                    coinController.Init(Manager.Data.GoldStat.HitGoldAmount.Value);
                }
            }               

            if (Manager.Data.GoldStat.KillGoldChance.Value > UnityEngine.Random.Range(0, 100))
            {
                coinObject = Instantiate(
                monsterStat.CoinPrefab,
                transform.position,
                Quaternion.identity
                );

                coinController = coinObject.GetComponent<Coin>();
                if (coinController != null)
                {
                    coinController.Init(monsterStat.GetCoinAmount * Manager.Data.GoldStat.KillGoldAmount.Value);
                }
            }
        }        
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damageable))
            {
                damageable.TakeDamage(AttackPower);
                Debug.Log($"{collision.gameObject.name}와 충돌하여 데미지 : {AttackPower}");
            }
        }
    }
}
