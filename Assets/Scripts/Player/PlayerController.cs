using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{  //�����̽��� �뽬 �����ؾ���

    [Header("�÷��̾�")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    public PlayerStatusController statusCon;    

    private PlayerWeaponController weaponCon;
    private Rigidbody2D rigid;
    private Vector2 movemoent;

    private void Start()
    {        
        weaponCon = GetComponent<PlayerWeaponController>();
        rigid = GetComponent<Rigidbody2D>();       
    }

    private void Update()
    {
        LookAtMouse();
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

    private void LookAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //���콺 ������ 
        Vector2 dir = mousePos - transform.position; //�÷��̾�� ���콺 ����

        if (dir.x >= 0)
        {
            spriteRenderer.flipX = false;

        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}