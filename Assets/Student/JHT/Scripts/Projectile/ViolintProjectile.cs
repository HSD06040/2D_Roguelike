using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolintProjectile : Projectile
{
    public LineRenderer lineRenderer;
    public Transform firePoint;

    private void Start()
    {
        DisableLaser();
    }

    public override void Init(Vector2 _targetPos)
    {
        targetPos = _targetPos;
    }

    private void Update()
    {
        if(targetPos.magnitude > 0.1)
        {
            EnableLaser();
            UpdateLaser();
        }

        if(targetPos.magnitude < 0.1)
        {
            DisableLaser();
        }
    }

    private void EnableLaser()
    {
        lineRenderer.enabled = true;
    }

    private void UpdateLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, targetPos);

        Vector2 direction = (Vector2)targetPos - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position,direction.normalized,direction.magnitude);
        
        if(hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
    }

    private void DisableLaser()
    {
        lineRenderer.enabled = false;
    }
}
