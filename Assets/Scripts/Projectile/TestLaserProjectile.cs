using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLaserProjectile : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    void Start()
    {
        DisableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            EnableLaser();
            
        }

        UpdateLaser();

        if(Input.GetButtonUp("Fire1"))
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
        if (!lineRenderer.enabled) return;
        var mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);

        lineRenderer.SetPosition(0, firePoint.position);

        lineRenderer.SetPosition(1, mousePos);
        
        Vector2 direction = mousePos - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude);
        Debug.Log($"targetPos : {mousePos.x}, {mousePos.y}");
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
