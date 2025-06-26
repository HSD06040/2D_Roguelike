using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class Projectile : MonoBehaviour
{
    protected int damage;
    [SerializeField] private float maxTime;
    public bool IsPass;
    [SerializeField] private Rigidbody2D rigid;
    protected Vector3 targetPos;


    public virtual void Init(Vector2 _targetPos, int _damage, float _speed)
    {        
        damage = _damage;        
        rigid.velocity = _targetPos * _speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            collision.GetComponent<IDamagable>().TakeDamage(damage);
        }
    }


    //다시

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (gameObject == null) return; // 트리거 돼서 사라질려는 동시에 Destroy가 호출될경우
    //    if (other.GetComponent<StatusController>() != target && other.gameObject.layer != 7) //layer == 7 플레이어로 설정
    //        return;
    //    if (target == null) return; //죽었을경우 return;
    //
    //    target.TakeDamage(damage);
    //
    //    if (IsPass == true) return;
    //    Destroy(gameObject, maxTime);
    //    
    //    //particle
    //}
}
