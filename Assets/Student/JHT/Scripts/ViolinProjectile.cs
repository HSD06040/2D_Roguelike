using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolinProjectile : Projectile
{
    public LineRenderer LineRender;

    [SerializeField][Range(1,50)] private int pointSize; //점의 갯수
    [SerializeField] private float vibration = 0.8f;
    [SerializeField] private LayerMask targetLayer;

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
        if (!LineRender.enabled) return;
        LineRender.positionCount = pointSize;

        Vector2 start = TestSingleton.JHT_TestInstance.playerWeapon.transform.position;
        Vector2 end = targetPos;

        Vector2 direction = end - start;
        Vector2 directionNormal = direction.normalized;

        RaycastHit2D hit = Physics2D.Raycast(start, directionNormal, 100,targetLayer);
        Vector2 rayPos = Vector2.zero;
        if(hit)
        {
            rayPos = hit.collider.transform.position;
            Debug.Log($"hitPos : {hit.collider.name} , {hit.collider.transform.position}");
        }


        for (int i = 0; i < pointSize; i++)
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

            
            if (i == 0)
            {
                //시작할 때 플레이어부터 시작
                LineRender.SetPosition(i, TestSingleton.JHT_TestInstance.playerWeapon.transform.position);
            }
            else if (i == pointSize - 1)
            {
                //hit.point가 적 또는 wall layer일경우 멈춤
                LineRender.SetPosition(pointSize - 1, hit.point);
            }
            else
            {
                LineRender.SetPosition(i, new Vector3(pointPos.x, pointPos.y + offsetY, 0));
            }
        }
    }

    private void PressLaser(bool value)
    {
        if(value)
        {
            LineRender.enabled = value;
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
 