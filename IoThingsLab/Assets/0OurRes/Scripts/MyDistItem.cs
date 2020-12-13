using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyDistItem
{
    private Camera mainCamera;

    public void Init(Camera camera) {
        Debug.Log("Dist Start!!!");
		mainCamera = camera;
    }

    public bool Triggered(Transform transform)
    {
        if (transform.position[0] - mainCamera.transform.position[0] > 0 && transform.position[0] - mainCamera.transform.position[0] < 5 && System.Math.Abs(transform.position[2] - mainCamera.transform.position[2]) < 2.2)
        {
            Debug.Log("In dist!");
            return true;
        }
        Debug.Log("Out of dist!");
        return false;
    }
}
