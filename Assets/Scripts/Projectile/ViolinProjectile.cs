using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ViolinProjectile : Projectile
{
    public LineRenderer LineRender;

    [SerializeField][Range(1,50)] private int pointSize; //Á¡ÀÇ °¹¼ö
    [SerializeField] private float vibration = 0.8f;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private LayerMask targetLayer;

    private RaycastHit2D hit;
    private bool canHit = true;
    private Coroutine hitDelay;

    [SerializeField] private float maxCount;
    private float delay = 0;
    private bool countFull;

    public override void Init(Vector2 _targetPos, int _damage, float _speed)
    {
        targetPos = _targetPos;
    }
    private void OnEnable()
    {
        Manager.Game.OnPress += PressLaser;
    }

    private void OnDisable()
    {
        Manager.Game.OnPress -= PressLaser;
    }


    private void Update()
    {
        if (delay <= maxCount)
        {
            delay += Time.deltaTime;
            countFull = false;
        }  
        else
        {
            countFull = true;
        }

        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UpdateLaser();
    }

    private void HitEnemy()
    {
        if (hit.collider != null && hit.collider.CompareTag("Monster"))
        {
            if(countFull)
            {
                hit.collider.gameObject.GetComponent<IDamagable>().TakeDamage(damage);
                GameObject obj = Instantiate(particlePrefab);
                obj.transform.position = hit.point;
                delay = 0;
            }
        }
        else
        {
            Debug.Log("Not hit");
        }
    }


    private void UpdateLaser()
    {
        if (!LineRender.enabled) return;
        LineRender.positionCount = pointSize;

        Vector2 start = Manager.Data.PassiveCon.orbitController.transform.position;
        Vector2 end = targetPos;

        Vector2 direction = end - start;
        Vector2 directionNormal = direction.normalized;

        hit = Physics2D.Raycast(start, directionNormal, 30, targetLayer);
        for (int i = 1; i < pointSize-1; i++)
        {
            float t = (float)i / (pointSize - 1);
            Vector2 pointPos = Vector3.Lerp(start, end, t);
            float offsetY = 0;

            if (i >= 5)
            {
                offsetY = 0;
            }
            else
            {
                offsetY = ((i % 2 == 0) ? 1 : -1) * vibration;
            }

            LineRender.SetPosition(i, new Vector3(pointPos.x, pointPos.y + offsetY, 0));
        }
        

        LineRender.SetPosition(0, Manager.Data.PassiveCon.orbitController.transform.position);

        if (hit.collider != null)
        {
            LineRender.SetPosition(pointSize - 1, hit.point);
            GameObject obj = Instantiate(particlePrefab);
            obj.transform.position = hit.point;
        }
        else
        {
            Debug.Log("hit point null");
            LineRender.SetPosition(pointSize - 1, end);
        }
        HitEnemy();
    }


    private void PressLaser(bool value)
    {
        if(value)
        {
            LineRender.enabled = value;
        }
        else
        {
            if(gameObject.activeInHierarchy)
            {
                StartCoroutine(DestroyObject());
            }
        }
    }


    private IEnumerator DestroyObject()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

}
