using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessing_Controller : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private Vignette vignette;
    private const float colorChangeSpeed = 4.0f;

    private Coroutine routine;

    private void Start()
    {
        volume.profile.TryGet<Vignette>(out vignette);
    }

    public void HitScreenRoutine()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
        routine = StartCoroutine(HitRoutine());
    }

    private IEnumerator HitRoutine()
    {
        Color start = vignette.color.value;
        Color end = Color.red;

        while (vignette.color.value != end)
        {
            Vector4 current = vignette.color.value;
            Vector4 target = end;
            vignette.color.value = (Color)Vector4.MoveTowards(current, target, colorChangeSpeed * Time.deltaTime);
            yield return null;
        }

        yield return Utile.GetDelay(.1f);

        while (vignette.color.value != start)
        {
            Vector4 current = vignette.color.value;
            Vector4 target = start;
            vignette.color.value = (Color)Vector4.MoveTowards(current, target, colorChangeSpeed * Time.deltaTime);
            yield return null;
        }

        vignette.color.value = start;
    }
}
