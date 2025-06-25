using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolinProjectile : Projectile
{
    public LineRenderer LineRender;

    [SerializeField][Range(50,100)] private int pointSize; //점의 갯수
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
        float directionDistance = direction.magnitude;
        Vector2 directionNormal = direction.normalized;

        RaycastHit2D hit = Physics2D.Raycast(start, directionNormal, directionDistance,targetLayer);

        //쭉 날라간다 하면 이거 필요없음
        //if (directionDistance < 5) pointSize = 10;
        //else if (directionDistance < 10 && directionDistance > 5) pointSize = 15;


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
                LineRender.SetPosition(pointSize - 1, targetPos);
            }
            else if(hit)
            {
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
