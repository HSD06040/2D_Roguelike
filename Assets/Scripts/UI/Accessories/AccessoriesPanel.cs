using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccessoriesPanel : AnimationUI_Base
{
    [SerializeField] private AccessoriesSlot[] slots;

    private void Awake()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].UpdateSlot();
        }
    }

    private void OnEnable()
    {
        Manager.Data.PlayerStatus.OnAccessoriesChanged += UpdateSlot;
    }

    private void OnDisable()
    {
        Manager.Data.PlayerStatus.OnAccessoriesChanged -= UpdateSlot;
    }

    private void UpdateSlot(int _idx, Accessories _ac)
    {
        if (_ac == null) return;

        slots[_idx].UpdateSlot(_ac);
    }
}
