using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolinProjectile : Projectile
{
    public LineRenderer LineRenderer;
    
    public override void Init(Vector2 _targetPos, int _damage, float _speed)
    {
        targetPos = _targetPos;
    }
    private void OnEnable()
    {
        TestSingleton.JHT_TestInstance.OnPress += PressLaser;
    }

    private void OnDisable()
    {
        TestSingleton.JHT_TestInstance.OnPress -= PressLaser;
    }


    private void Update()
    {
        UpdateLaser();
    }

    private void UpdateLaser()
    {
        if (!LineRenderer.enabled) return;

        Vector2 mousePos = (Vector2)targetPos;

        LineRenderer.SetPosition(0, TestSingleton.JHT_TestInstance.playerWeapon.transform.position);
        LineRenderer.SetPosition(1, mousePos);

        Vector2 direction = mousePos - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude);
        Debug.Log($"targetPos : {mousePos.x} , {mousePos.y}");
        if (hit)
        {
            LineRenderer.SetPosition(1, hit.point);
        }
    }


    private void PressLaser(bool value)
    {
        if(value)
        {
            LineRenderer.enabled = value;
        }
        else
        {
            if(this.gameObject.activeInHierarchy)
            {
                StartCoroutine(DestroyObject());
            }
        }
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
    }

}
