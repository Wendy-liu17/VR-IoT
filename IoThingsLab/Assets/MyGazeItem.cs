using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyGazeItem : MySensorItem
{
    private Camera mainCamera;

	void Start() {
        Debug.Log("Gaze Start!!!");
		if (!mainCamera)
			mainCamera = Camera.main;
        UnityAction triggeredAction = new UnityAction(func1);
        UnityAction untriggeredAction = new UnityAction(func2);
        setTriggered(triggeredAction);
        setUnTriggered(untriggeredAction);
	}

	void Update() {
        Vector3 vec = mainCamera.WorldToViewportPoint(transform.position);
        if (vec.x > 0 && vec.x < 1 && vec.y > 0 && vec.y < 1 && vec.z > mainCamera.nearClipPlane && vec.z < mainCamera.farClipPlane)
            SensorTrigger();
        else
            SensorUntrigger();
	}

    void func1() {
        Debug.Log("In sight!");
    }

    void func2() {
        Debug.Log("Out of sight!");
    }
}
