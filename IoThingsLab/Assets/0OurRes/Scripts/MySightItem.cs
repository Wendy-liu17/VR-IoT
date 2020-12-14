using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MySightItem
{
    private Camera mainCamera;

    public void Init(Camera camera) {
        Debug.Log("Sight Start!!!");
		mainCamera = camera;
    }

    public bool Triggered(Transform transform)
    {
        Vector3 vec = mainCamera.WorldToViewportPoint(transform.position);
        if (vec.x > 0 && vec.x < 1 && vec.y > 0 && vec.y < 1 && vec.z > mainCamera.nearClipPlane && vec.z < mainCamera.farClipPlane)
        {
            Debug.Log("In sight!");
            return true;
        }
        Debug.Log("Out of sight!");
        return false;
    }
}
