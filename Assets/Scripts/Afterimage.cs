using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    [SerializeField] private GameObject spritePrefab;
    [SerializeField] private Transform playerPos;
    [SerializeField] private int numberOfImages;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float fadeDuration;

    private PlayerController playerController;
    private float smoothness;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void DashAfterImageOn()
    {
        if(playerController.spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        StartCoroutine(AfterImageOnCoroutine());
    }

    private IEnumerator AfterImageOnCoroutine()
    {
        smoothness = playerController.dashDuration / numberOfImages;

        for (int i = 0; i < numberOfImages; i++)
        {
            yield return new WaitForSeconds(smoothness);
            //GameObject afterImage = Instantiate(spritePrefab, playerPos.position, Quaternion.identity);
            GameObject afterImage = Manager.Resources.Instantiate(spritePrefab, playerPos.position, Quaternion.identity, true);

            StartCoroutine(FadeOutCoroutine(afterImage));
        }
    }

    private IEnumerator FadeOutCoroutine(GameObject afterImage)
    {
        SpriteRenderer image = afterImage.GetComponent<SpriteRenderer>();
        Color ogColor = image.color;

        float timer = 0f;
        
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.8f, 0f, timer /  fadeDuration);
            image.color = new Color(ogColor.r, ogColor.g, ogColor.b, alpha);
            yield return null;
        }
        //Destroy(image);
        Manager.Resources.Destroy(image.gameObject);
    }
}

