using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private const float duration = .5f;
    [SerializeField] private GameObject popupTextPrefab;
    [SerializeField] private SpriteRenderer playerRenderer;

    private Coroutine playerTakeDamageCor;
    private Color playerColor;
    private WaitForSeconds delay = new WaitForSeconds(0.2f);
    private void Start()
    {
        playerColor = playerRenderer.material.color;
    }
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
        yield return null;
        playerRenderer.material.color = Color.red;
        yield return delay;
        playerRenderer.material.color = playerColor;
        yield return delay;
        playerRenderer.material.color = Color.red;
        yield return delay;
        playerRenderer.material.color = playerColor;
        yield return delay;
        playerRenderer.material.color = Color.red;
        yield return delay;
        playerRenderer.material.color = playerColor;

        if(playerTakeDamageCor != null)
        {
            StopCoroutine(playerTakeDamageCor);
            playerTakeDamageCor = null;
        }
    }
}
