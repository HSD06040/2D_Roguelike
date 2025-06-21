using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private int damage;
    StatusController target = null;
    [SerializeField] private float maxTime;
    [SerializeField] private float speed = 3;
    private Rigidbody rigid;
    private Transform startPos;
    public bool IsPass;

    private Vector3 targetPos;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();  
    }

    public void Init(Vector3 _targetPos)
    {
        //targetPos = _targetPos;
        rigid.velocity = _targetPos * speed;
    }
    
    //private void Update()
    //{
    //    //점찍은 방향으로 이동
    //
    //    //끝까지 감
    //    //transform.position += targetPos.normalized * Time.deltaTime * Speed;
    //
    //    //해당 위치에서 없어짐
    //    transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.1f);
    //
    //    if (transform.position == targetPos)
    //    {
    //        Destroy(this.gameObject, 0.3f); // PooledObject.ReturnPool()자리
    //    }
    //}

    //파티클 생성 위치


    //다시
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject == null) return; // 트리거 돼서 사라질려는 동시에 Destroy가 호출될경우
        if (IsPass == true) return;
        if (other.GetComponent<StatusController>() != target && other.gameObject.layer != 7) //layer == 7 플레이어로 설정
            return;
        if (target == null) return; //죽었을경우 return;

        target.TakeDamage(damage);
        Destroy(gameObject, maxTime);
        
        //particle
    }
}
