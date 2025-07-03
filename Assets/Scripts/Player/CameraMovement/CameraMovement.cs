using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform roomCenter;
    [SerializeField] private float cameraMoveSpeed = 5f;

    private bool isPlayerinTheRoom = false;
    private bool isCameraMoving = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isPlayerinTheRoom)
        {
            isPlayerinTheRoom = true;
            isCameraMoving = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerinTheRoom = false;
        }
    }

    private void Update()
    {
        if(isCameraMoving && isPlayerinTheRoom)
        {
            Camera cam = Camera.main;
            Vector3 targetPos = new Vector3(roomCenter.position.x, roomCenter.position.y, cam.transform.position.z);
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, targetPos, cameraMoveSpeed * Time.deltaTime);

            if(Vector3.Distance(cam.transform.position, targetPos) < 0.01f)
            {
                isCameraMoving = false;
            }
        }
    }
}
