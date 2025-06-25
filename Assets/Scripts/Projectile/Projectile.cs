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


    //�ٽ�

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (gameObject == null) return; // Ʈ���� �ż� ��������� ���ÿ� Destroy�� ȣ��ɰ��
    //    if (other.GetComponent<StatusController>() != target && other.gameObject.layer != 7) //layer == 7 �÷��̾�� ����
    //        return;
    //    if (target == null) return; //�׾������ return;
    //
    //    target.TakeDamage(damage);
    //
    //    if (IsPass == true) return;
    //    Destroy(gameObject, maxTime);
    //    
    //    //particle
    //}
}
