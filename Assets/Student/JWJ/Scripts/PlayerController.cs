using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{  //스페이스바 대쉬 구현해야함

    [Header("플레이어")]
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rigid;
    private Vector2 movemoent;
    Camera cam;

    [Header("무기")]
    [SerializeField] private GameObject defaultWeapon;
    private List<GameObject> ownedWeapons = new List<GameObject>(); //획득한 악기 리스트
    private GameObject currentWeapon;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentWeapon != null)
            {
                // 선택중인 악기의 공격;
            }

            else
            {
                // 기본무기의 공격;
            }
        }

        WeaponSwap();
    }

    public void AddWeapon(GameObject weaponPrefab)
    {
        GameObject newWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        ownedWeapons.Add(newWeapon);
        Debug.Log("무기추가" + newWeapon.name);
    }

    private void WeaponSwap()
    {
        for (int i = 0; i < ownedWeapons.Count; i++) //소지한 악기의 개수만큼 키 할당
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) //자동 키 할당
            {
                SelectWeapon(i); //SelectWeapon에 매게변수 넘겨줌
                Debug.Log((i + 1) + "번 악기 선택");
            } 
        }

        if (ownedWeapons.Count == 0) //무기가 없을땐 currentWeapon = null 처리해서 기본무기 사용
        {
            currentWeapon = null;
        }
    }

    private void SelectWeapon(int index)
    {
        if (index >= ownedWeapons.Count)  //가진수보다 큰 숫자의 키를 누르면 리턴
        {
            return;
        }

        currentWeapon = ownedWeapons[index];  //선택한 무기 = currentWeapon
        Debug.Log((index +1) + "번 무기 사용중");

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

}
