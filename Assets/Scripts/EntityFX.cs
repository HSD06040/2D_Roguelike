using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private const float duration = .5f;
    [SerializeField] private GameObject popupTextPrefab;
    [SerializeField] private SpriteRenderer spriteRender;

    private Coroutine playerTakeDamageCor;
    private WaitForSeconds delay = new WaitForSeconds(0.2f);

    public void CreatePopupText(float damage)
    {
        GameObject obj = Manager.Pool.GetPopup(popupTextPrefab, transform.position + new Vector3(Random.Range(-.5f,.5f), Random.Range(-.2f, .2f)));      
        obj.GetComponent<DamagePopupText>().Init(damage.ToString("F1"));
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
        Color playerColor = spriteRender.color;
        yield return null;
        spriteRender.material.color = Color.red;
        yield return delay;
        spriteRender.material.color = playerColor;
        yield return delay;
        spriteRender.material.color = Color.red;
        yield return delay;
        spriteRender.material.color = playerColor;
        yield return delay;
        spriteRender.material.color = Color.red;
        yield return delay;
        spriteRender.material.color = playerColor;

        if(playerTakeDamageCor != null)
        {
            StopCoroutine(playerTakeDamageCor);
            playerTakeDamageCor = null;
        }
    }
}
