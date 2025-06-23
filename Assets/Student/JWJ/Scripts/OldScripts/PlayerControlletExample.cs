using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControllerExample : DamageSystemExample
{
    [Header("�÷��̾�")]
    [SerializeField] private float moveSpeed;

    [Header("�߻�ü")]
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

    private void Move()  //�÷��̾� �̵� �Լ�
    {
        movemoent.x = Input.GetAxisRaw("Horizontal");
        movemoent.y = Input.GetAxisRaw("Vertical");
        rigid.velocity = movemoent.normalized * moveSpeed;
    }

    private void Fire()
    {
        Vector3 mouseDirection = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        //ScreenToWorldPoint() ȭ����ǥ���� ���� ��ǥ�� ��ȯ�ϴ� �Լ�
        Vector2 fireDir = (mouseDirection - transform.position).normalized; //�߻� ���� 
        GameObject bullet = Instantiate(bulletPrefab); 
        bullet.transform.position = firePoint.transform.position; // firePoint �� ������ ��ġ����
        bullet.gameObject.GetComponent<Rigidbody2D>().AddForce(fireDir * bulletSpeed, ForceMode2D.Impulse);
    }
}
