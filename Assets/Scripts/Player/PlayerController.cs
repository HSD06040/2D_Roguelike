using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{ 

    [Header("플레이어")]
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] private float dashSpeed; //대쉬스피드
    [SerializeField] public float dashDuration; //대쉬시간
    [SerializeField] private float dashCoolDown;

    [SerializeField] private SpriteRenderer armSprite;
    private Animator bodyAnimator;
    [SerializeField]private Animator armAnimator;

    private Afterimage afterimage;
    private bool isDashing = false;
    private bool canDash = true;

    public PlayerStatusController statusCon;
    public Vector2 MousePos;

    private PlayerWeaponController weaponCon;
    private Rigidbody2D rigid;
    private Vector2 movemoent;

    private IInteractable interactableTarget;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {        
        weaponCon = GetComponent<PlayerWeaponController>();
        rigid = GetComponent<Rigidbody2D>();
        afterimage = GetComponent<Afterimage>();
        bodyAnimator = GetComponent<Animator>();
        //armAnimator = transform.Find("Arm").GetComponent<Animator>();
    }

    private void Update()
    {
        LookAtMouse();
        PlayerDash();
        PlayerInteraction();
        PlayerAnimation();

        if (Input.GetMouseButtonDown(0))
        {
            armAnimator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        if(!isDashing)
        {
            Move();
        }
    }

    private void Move()  //플레이어 이동 함수
    {
        movemoent.x = Input.GetAxisRaw("Horizontal");
        movemoent.y = Input.GetAxisRaw("Vertical");
        rigid.velocity = movemoent.normalized * statusCon.status.Speed.Value;
    }   

    private void LookAtMouse()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스 포지션 
        Vector2 dir = MousePos - (Vector2)transform.position; //플레이어에서 마우스 방향

        if (dir.x >= 0)
        {
            spriteRenderer.flipX = true;
            armSprite.flipX = true;

        }
        else
        {
            spriteRenderer.flipX = false;
            armSprite.flipX = false;

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
        afterimage.DashAfterImageOn();
        isDashing = true;
        canDash = false;

        Vector2 dasDir = movemoent.normalized;
        rigid.velocity = dasDir * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCoolDown);

        canDash = true;
    }

    ////interaction

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactableTarget = interactable;
            interactableTarget.UiOn();
            //Debug.Log("E키로 상호작용 가능한 타겟: ");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            if(interactableTarget == interactable)
            {
                interactableTarget.UiOff();
                interactableTarget = null;
                //Debug.Log("멀어져서 상호작용 불가능해진 타겟: ");
            }
            
        }
    }

    private void PlayerInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactableTarget != null)
        {
            interactableTarget.Interact();
        }
    }

    private void PlayerAnimation()
    {
        float currentSpeed = rigid.velocity.magnitude;
        bodyAnimator.SetBool("isWalking", currentSpeed > 0.1f && !isDashing);  //걷기 애니메이션
    }
}