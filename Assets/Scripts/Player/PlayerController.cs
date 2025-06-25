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
    [SerializeField] private float dashSpeed; //��ý��ǵ�
    [SerializeField] private float dashDuration; //��ýð�
    [SerializeField] private float dashCoolDown;

    private bool isDashing = false;
    private bool canDash = true;

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
        PlayerDash();
    }

    private void FixedUpdate()
    {
        if(!isDashing)
        {
            Move();
        }

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

    private void PlayerDash()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canDash && !isDashing && movemoent != Vector2.zero)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        canDash = false;

        Vector2 dasDir = movemoent.normalized;
        rigid.velocity = dasDir * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCoolDown);

        canDash = true;
    }
}