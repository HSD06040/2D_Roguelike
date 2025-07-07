using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ViolinProjectile : Projectile
{
    [SerializeField] private GameObject laser;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private ParticleSystem elecParticle;
    [SerializeField] private GameObject trigParticle;
    [SerializeField] private AudioClip[] violinSFX;

    private ParticleSystem electObj;
    private RaycastHit2D[] hits;
    private Coroutine hitDelay;

    [SerializeField] private float maxCount;
    private float delay = 0;
    private bool countFull;

    [SerializeField] private float particleMaxCount;
    private float particleDelay = 0;
    private bool particleCountFull;

    private GameObject curSFX;


    public override void Init(Vector2 _targetPos, float _damage, float _speed)
    {
        base.Init(targetPos, Manager.Data.PlayerStatus.TotalDamage,0);
        targetPos = _targetPos;
    }
    private void OnEnable()
    {
        electObj = Instantiate(elecParticle, transform);
        Manager.Game.IsPress.AddEvent(PressLaser);
    }

    private void OnDisable()
    {
        Manager.Game.IsPress.RemoveEvent(PressLaser);
        Destroy(electObj);
    }

    private void Start()
    {
        StartCoroutine(SFXPlay());
    }

    private IEnumerator SFXPlay()
    {
        while(Manager.Game.IsPress.Value)
        {
            while(true)
            {
                rand = Random.Range(0, violinSFX.Length);
                Manager.Audio.PlaySFX($"Weapon/Violin/{violinSFX[rand].name}", transform.position);
                yield return Utile.GetDelay(violinSFX[rand].length);
                continue;
            }
        }
        yield return null;
    }

    private void Update()
    {
        #region 공격 쿨타임
        if (delay <= maxCount)
        {
            delay += Time.deltaTime;
            countFull = false;
        }  
        else
        {
            countFull = true;
        }
        #endregion

        #region 파티클 쿨타임
        if (particleDelay <= particleMaxCount)
        {
            particleDelay += Time.deltaTime;
            particleCountFull = false;
        }
        else
        {
            particleCountFull = true;
        }
        #endregion


        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UpdateLaser();
    }

    private void UpdateLaser()
    {
        if (!Manager.Game.IsPress.Value)
        {
            return;
        }

        Vector2 start = Manager.Data.PassiveCon.orbitController.transform.position;
        Vector2 end = targetPos;

        Vector2 direction = end - start;
        Vector2 directionNormal = direction.normalized;

        hits = Physics2D.RaycastAll(start, directionNormal, 30, targetLayer);

        electObj.transform.position = Manager.Data.PassiveCon.orbitController.transform.position;

        float angle = Mathf.Atan2(directionNormal.y, directionNormal.x) * Mathf.Rad2Deg;
        electObj.transform.localRotation = Quaternion.Euler(0, 0, angle);


        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Monster"))
            {
                if (countFull)
                {
                    hit.collider.gameObject.GetComponent<IDamagable>().TakeDamage(damage);
                    delay = 0; 
                    GameObject obj = Instantiate(trigParticle);
                    obj.transform.position = hit.point;
                    particleCountFull = true;
                    particleDelay = 0;
                }
                else
                {
                    Debug.Log("Not Hit Enemy");
                }
            }

            if (hit.collider.CompareTag("Wall"))
            {
                if (particleCountFull)
                {
                    GameObject obj = Instantiate(trigParticle);
                    obj.transform.position = hit.point;
                    particleDelay = 0;
                }

                float distance = Vector2.Distance(start, hit.point);
                electObj.transform.localScale = new Vector2(distance, electObj.transform.localScale.y);

                break;
            }
            
        }


    }

    private void PressLaser(bool value)
    {
        if(value)
        {
            laser.SetActive(true);
        }
        else
        {
            if(gameObject.activeInHierarchy)
            {
                StartCoroutine(DestroyObject());
            }
        }
    }


    private IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

}
