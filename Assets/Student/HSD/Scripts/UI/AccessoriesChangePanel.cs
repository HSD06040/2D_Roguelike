using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessoriesChangePanel : AnimationUI_Base
{
    [SerializeField] private Button changeButton;
    [SerializeField] private Button dropButton;
    [SerializeField] private GameObject background;
    [SerializeField] private AccessoriesChangeSlot newSlot;
    [SerializeField] private AccessoriesChangeSlot[] oldSlot;
    private Accessories newAccessories;
    public ToolTip toolTip;
    public int selectSlot;

    private void Start()
    {
        changeButton.onClick.AddListener(ChangeAccessories);
        dropButton.onClick.AddListener(Close);

        newSlot.Setup(this);
        for (int i = 0; i < oldSlot.Length; i++)
        {
            oldSlot[i].Setup(this, i);
        }
    }

    private void OnEnable()
    {
        dropButton.interactable = true;

        for (int i = 0; i < oldSlot.Length; i++)
        {
            oldSlot[i].Select(false);
        }
    }

    private void ChangeAccessories()
    {
        Manager.Data.PlayerStatus.ChangeAccessories(newAccessories, selectSlot);
        Close();
    }

    public void OpenChangePanel(Accessories newAc)
    {
        Open();
        background.SetActive(true);
        newAccessories = newAc;
        newSlot.UpdateSlot(newAc);

        for (int i = 0; i < oldSlot.Length; i++)
        {
            oldSlot[i].UpdateSlot(Manager.Data.PlayerStatus.PlayerAccessories[i]);
        }
    }    

    public void Select(int _idx)
    {
        oldSlot[selectSlot].Select(false);
        selectSlot = _idx;
        changeButton.interactable = true;
        oldSlot[selectSlot].Select(true);
    }
    public override void Close()
    {
        base.Close();

        background.SetActive(false);

        changeButton.interactable = false;
        dropButton.interactable = false;
    }
}
