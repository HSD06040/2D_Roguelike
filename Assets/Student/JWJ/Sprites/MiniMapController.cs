using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] GameObject miniMap;
    private void Start()
    {
        miniMap.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            miniMap.SetActive(true);
        }
        else
        {
            miniMap.SetActive(false);
        }
    }
}
