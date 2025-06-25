using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private const float duration = .5f;
    [SerializeField] private GameObject popupTextPrefab;

    public void CreatePopupText(int damage)
    {
        GameObject obj = Manager.Pool.GetPopup(popupTextPrefab, transform.position);      
        obj.GetComponent<DamagePopupText>().Init(damage.ToString());
    }

    private IEnumerator HitRoutine()
    {
        yield return null;
    }
}
