using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private const float duration = .5f;
    [SerializeField] private GameObject popupTextPrefab;

    private Coroutine playerTakeDamageCor;
    private WaitForSeconds delay = new WaitForSeconds(0.2f);

    public void CreatePopupText(int damage)
    {
        GameObject obj = Manager.Pool.GetPopup(popupTextPrefab, transform.position);      
        obj.GetComponent<DamagePopupText>().Init(damage.ToString());
    }

    public void CreateTakeDamageMaterial(SpriteRenderer _playerSprite)
    {
        if(playerTakeDamageCor == null)
        {
            playerTakeDamageCor = StartCoroutine(HitRoutine(_playerSprite));
        }
    }

    private IEnumerator HitRoutine(SpriteRenderer _playerSprite)
    {
        Color playerColor = _playerSprite.color;
        yield return null;
        _playerSprite.material.color = Color.red;
        yield return delay;
        _playerSprite.material.color = playerColor;
        yield return delay;
        _playerSprite.material.color = Color.red;
        yield return delay;
        _playerSprite.material.color = playerColor;
        yield return delay;
        _playerSprite.material.color = Color.red;
        yield return delay;
        _playerSprite.material.color = playerColor;

        if(playerTakeDamageCor != null)
        {
            StopCoroutine(playerTakeDamageCor);
            playerTakeDamageCor = null;
        }
    }
}
