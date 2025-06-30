using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UseItem", menuName = "Item/UseItem")]
public class UseItem : Item
{
    [SerializeField, Tooltip("스텟 삭제 유무")] 
    private bool isRemove;

    [SerializeField] private StatEffectStruct[] addStat;
    [SerializeField] private StatEffectStruct[] removeStat;

    public void Execute()
    {
        for (int i = 0; i < addStat.Length; i++)
        {
            Manager.Data.PlayerStatus.AddStat(addStat[i].statType, addStat[i].value, name);
        }

        for (int i = 0; i < removeStat.Length; i++)
        {
            Manager.Data.PlayerStatus.AddStat(removeStat[i].statType, -removeStat[i].value, name);
        }
    }

    public void Revoke()
    {
        
    }
}
