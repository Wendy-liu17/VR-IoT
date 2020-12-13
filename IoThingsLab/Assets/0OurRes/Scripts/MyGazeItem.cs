using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyGazeItem : MonoBehaviour {
    public UnityEvent event1;
    public UnityEvent event2;

    private Camera mainCamera;
    private bool isTriggered = false;

	void Start() {
        Debug.Log("Gaze Start!!!");
		if (!mainCamera)
			mainCamera = Camera.main;
	}

	void Update() {
        Vector3 vec = mainCamera.WorldToViewportPoint(transform.position);
        if (vec.x > 0 && vec.x < 1 && vec.y > 0 && vec.y < 1 && vec.z > mainCamera.nearClipPlane && vec.z < mainCamera.farClipPlane) {
            if (isTriggered) {
                isTriggered = false;
                Debug.Log("In sight!");
                event1.Invoke();
            }
        }
        else {
            if (!isTriggered) {
                isTriggered = true;
                Debug.Log("Out of sight!");
                event2.Invoke();
            }
        }
	}
}
