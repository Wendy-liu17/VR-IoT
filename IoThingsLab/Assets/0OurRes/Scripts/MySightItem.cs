using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MySightItem : MonoBehaviour
{
    public UnityEvent onGrab;
    public ThrowEvent onThrow;
    public int curIdx = 0;

    private Camera mainCamera;
    private bool isTriggered = false;

    void Start()
    {
        Debug.Log("Sight Start!!!");
        if (!mainCamera)
            mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 vec = mainCamera.WorldToViewportPoint(transform.position);
        if (vec.x > 0 && vec.x < 1 && vec.y > 0 && vec.y < 1 && vec.z > mainCamera.nearClipPlane && vec.z < mainCamera.farClipPlane)
        {
            if (isTriggered)
            {
                isTriggered = false;
                Debug.Log("In sight!");
                onThrow.Invoke(curIdx);
            }
        }
        else
        {
            if (!isTriggered)
            {
                isTriggered = true;
                Debug.Log("Out of sight!");
                onGrab.Invoke();
            }
        }
    }
}
