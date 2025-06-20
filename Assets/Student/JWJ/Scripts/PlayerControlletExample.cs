using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControllerExample : DamageSystemExample
{
    [Header("플레이어")]
    [SerializeField] private float moveSpeed;

    [Header("발사체")]
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    
    private Rigidbody2D rigid;
    private Vector2 movemoent;
    Camera cam;
    


    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()  //플레이어 이동 함수
    {
        movemoent.x = Input.GetAxisRaw("Horizontal");
        movemoent.y = Input.GetAxisRaw("Vertical");
        rigid.velocity = movemoent.normalized * moveSpeed;
    }

    private void Fire()
    {
        Vector3 mouseDirection = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        //ScreenToWorldPoint() 화면좌표에서 월드 좌표로 변환하는 함수
        Vector2 fireDir = (mouseDirection - transform.position).normalized; //발사 방향 
        GameObject bullet = Instantiate(bulletPrefab); 
        bullet.transform.position = firePoint.transform.position; // firePoint 로 프리팹 위치지정
        bullet.gameObject.GetComponent<Rigidbody2D>().AddForce(fireDir * bulletSpeed, ForceMode2D.Impulse);
    }
}
