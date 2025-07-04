using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class Projectile : MonoBehaviour
{
    protected float damage;
    [SerializeField] private float maxTime;
    public bool IsPass;
    [SerializeField] private Rigidbody2D rigid;
    protected Vector3 targetPos;
    public GameObject ParticlePrefab;

    private void Start()
    {
        StartCoroutine(SpawnTime());
    }

    public virtual void Init(Vector2 _targetPos, float _damage, float _speed)
    {        
        damage = Manager.Data.PlayerStatus.TotalDamage;        
        rigid.velocity = _targetPos * _speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Monster"))
        {
            collision.GetComponent<IDamagable>().TakeDamage(damage);
            if(!IsPass && gameObject != null)
            {
                Destroy(gameObject);
            }
            if (ParticlePrefab == null) return;
            GameObject obj = Instantiate(ParticlePrefab);
            obj.transform.position = collision.transform.position;
            Destroy(obj,0.4f);
        }

        if (collision.CompareTag("Wall"))
        {
            if (ParticlePrefab == null) return;
            GameObject obj = Instantiate(ParticlePrefab);
            obj.transform.position = gameObject.transform.position;
            Destroy(obj, 0.4f);
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(3f);
        yield return new WaitForNextFrameUnit();

        if(gameObject != null)
            Destroy(this.gameObject);
    }
}
