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

        if(Input.GetButton("Fire1"))
        {
            UpdateLaser();
        }

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
        var mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);

        lineRenderer.SetPosition(0, firePoint.position);

        lineRenderer.SetPosition(1, mousePos);
    }

    private void DisableLaser()
    {
        lineRenderer.enabled = false;
    }
}
