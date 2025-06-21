using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    StatusController target = null;

    [SerializeField] private float maxTime;
    [SerializeField] private GameObject bulletModel;
    [SerializeField] private float Speed = 1;
    private Rigidbody rigid;
    private Transform startPos;

    private Vector3 targetPos;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    public void Init(Transform _projectilePos, Vector3 _targetPos)
    {
        GameObject obj = Instantiate(bulletModel, _projectilePos);
        targetPos = _targetPos;
        startPos = _projectilePos.transform;
    }

    private void Update()
    {
        //점찍은 방향으로 이동

        //끝까지 감
        //transform.position += targetPos.normalized * Time.deltaTime * Speed;

        //해당 위치에서 없어짐
        transform.position = Vector3.MoveTowards(startPos.position, targetPos, 0.1f);
    }

    //파티클 생성 위치
    private Transform SetParticlePos()
    {
        return null;
    }

    //다시
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject == null) return; // 트리거 돼서 사라질려는 동시에 Destroy가 호출될경우
        if (other.GetComponent<StatusController>() != target)
            return;
        if (target == null) return; //죽었을경우 return;

        target.TakeDamage(damage);
        
        //particle
    }
}
