using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{  //�����̽��� �뽬 �����ؾ���

    [Header("�÷��̾�")]
    public PlayerStatusController statusCon;    

    private PlayerWeaponController weaponCon;
    private Rigidbody2D rigid;
    private Vector2 movemoent;

    private void Start()
    {        
        weaponCon = GetComponent<PlayerWeaponController>();
        rigid = GetComponent<Rigidbody2D>();       
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()  //�÷��̾� �̵� �Լ�
    {
        movemoent.x = Input.GetAxisRaw("Horizontal");
        movemoent.y = Input.GetAxisRaw("Vertical");
        rigid.velocity = movemoent.normalized * statusCon.status.Speed.Value;
    }   
}
