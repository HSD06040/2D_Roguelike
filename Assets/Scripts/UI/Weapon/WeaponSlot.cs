using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private Image[] slots;   // 추가됨!!! 슬롯 배경 (회색/흰색)
    [SerializeField] private Image[] weaponIcons;        // 추가됨!!! 아이템 아이콘

    [SerializeField] private Sprite selectedSlotSprite;   // 추가됨!!! 선택됨 슬롯 배경 (흰색)
    [SerializeField] private Sprite unselectedSlotSprite; // 추가됨!!! 선택 안됨 배경 (회색)

    private WeaponSlotPresenter presenter;

    private void Awake()
    {
        presenter = new WeaponSlotPresenter(this);
    }

    private void Start()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = unselectedSlotSprite;   //슬롯은 전부 비활성화 스프라이트로 시작
            weaponIcons[i].gameObject.SetActive(false); //아이콘은 다 꺼줌
        }
    }

    private void OnEnable()
    {
        presenter.AddEvent();
    }

    private void OnDisable()
    {
        presenter.RemoveEvent();
    }

    public void ChangeSlot(int _idx)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if( i == _idx )
            {
                slots[i].sprite = selectedSlotSprite; //선택된건 흰색
            }
            else
            {
                slots[i].sprite = unselectedSlotSprite;
            }
        }
    }

    public void UpdateWeaponSlot(int _idx, MusicWeapon _weapon)
    {
        if (_weapon == null)
        {
            weaponIcons[_idx].gameObject.SetActive(false); // 아이템 없으면 아이콘 비활성화
            return;
        }

        weaponIcons[_idx].gameObject.SetActive(true); // 아이템 있으면 아이콘 활성화
        weaponIcons[_idx].sprite = _weapon.WeaponData.icon; // 아이콘 표시
    }
}
