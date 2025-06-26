using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private const float duration = .5f;
    [SerializeField] private GameObject popupTextPrefab;
    [SerializeField] private SpriteRenderer playerSpriteRender;

    private Coroutine playerTakeDamageCor;
    private WaitForSeconds delay = new WaitForSeconds(0.2f);

    public void CreatePopupText(int damage)
    {
        GameObject obj = Manager.Pool.GetPopup(popupTextPrefab, transform.position);      
        obj.GetComponent<DamagePopupText>().Init(damage.ToString());
    }

    public void CreateTakeDamageMaterial()
    {
        if(playerTakeDamageCor == null)
        {
            playerTakeDamageCor = StartCoroutine(HitRoutine());
        }
    }

    private IEnumerator HitRoutine()
    {
        Color playerColor = playerSpriteRender.color;
        yield return null;
        playerSpriteRender.material.color = Color.red;
        yield return delay;
        playerSpriteRender.material.color = playerColor;
        yield return delay;
        playerSpriteRender.material.color = Color.red;
        yield return delay;
        playerSpriteRender.material.color = playerColor;
        yield return delay;
        playerSpriteRender.material.color = Color.red;
        yield return delay;
        playerSpriteRender.material.color = playerColor;

        if(playerTakeDamageCor != null)
        {
            StopCoroutine(playerTakeDamageCor);
            playerTakeDamageCor = null;
        }
    }
}
