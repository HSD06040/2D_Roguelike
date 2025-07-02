using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionIndicatorController : MonoBehaviour
{
    public void SetSize(float explosionRadius, float desiredScaleX, float desiredScaleY)
    {
        float longerSide = Mathf.Max(desiredScaleX, desiredScaleY);
        if (longerSide == 0) return; 

        float diameter = explosionRadius * 2;
        float scaleMultiplier = diameter / longerSide;

        float finalX = desiredScaleX * scaleMultiplier;
        float finalY = desiredScaleY * scaleMultiplier;

        transform.localScale = new Vector3(finalX, finalY, 1f);
    }
}