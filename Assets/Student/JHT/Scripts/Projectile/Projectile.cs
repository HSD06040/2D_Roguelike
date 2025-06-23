using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class Projectile : MonoBehaviour
{
    private int damage = 0;
    [SerializeField] private float maxTime;
    [SerializeField] private float speed = 3;
    public bool IsPass;
    [SerializeField] private Rigidbody2D rigid;
    protected Vector3 targetPos;


    public virtual void Init(Vector2 _targetPos)
    {
        rigid.velocity = _targetPos * speed;
        //targetPos.z = 0;
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
