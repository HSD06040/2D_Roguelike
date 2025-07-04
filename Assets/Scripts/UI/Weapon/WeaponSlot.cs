using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private Image[] slots;   // �߰���!!! ���� ��� (ȸ��/���)
    [SerializeField] private Image[] weaponIcons;        // �߰���!!! ������ ������

    [SerializeField] private Sprite selectedSlotSprite;   // �߰���!!! ���õ� ���� ��� (���)
    [SerializeField] private Sprite unselectedSlotSprite; // �߰���!!! ���� �ȵ� ��� (ȸ��)

    private WeaponSlotPresenter presenter;

    private void Awake()
    {
        presenter = new WeaponSlotPresenter(this);
    }

    private void Start()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = unselectedSlotSprite;   //������ ���� ��Ȱ��ȭ ��������Ʈ�� ����
            weaponIcons[i].gameObject.SetActive(false); //�������� �� ����
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
                slots[i].sprite = selectedSlotSprite; //���õȰ� ���
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
            weaponIcons[_idx].gameObject.SetActive(false); // ������ ������ ������ ��Ȱ��ȭ
            return;
        }

        weaponIcons[_idx].gameObject.SetActive(true); // ������ ������ ������ Ȱ��ȭ
        weaponIcons[_idx].sprite = _weapon.WeaponData.icon; // ������ ǥ��
    }
}
