using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private Image[] slots;
    [SerializeField] private Image[] weaponIcons;

    [SerializeField] private Sprite selectedSlotSprite;
    [SerializeField] private Sprite unselectedSlotSprite;

    private WeaponSlotPresenter presenter;

    private void Awake()
    {
        presenter = new WeaponSlotPresenter(this);
    }

    private void Start()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = unselectedSlotSprite;
            weaponIcons[i].gameObject.SetActive(false);
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
                slots[i].sprite = selectedSlotSprite;
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
            weaponIcons[_idx].gameObject.SetActive(false);
            return;
        }

        weaponIcons[_idx].gameObject.SetActive(true);
        weaponIcons[_idx].sprite = _weapon.WeaponData.icon;
    }
}
