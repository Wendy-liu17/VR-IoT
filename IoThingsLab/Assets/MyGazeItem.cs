using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGazeItem : MonoBehaviour
{
    private Camera mainCamera;	
	
	void Start() {
        Debug.Log("Gaze Start!!!");
		if (!mainCamera)
			mainCamera = Camera.main;
	}
	
	void Update() {
        Vector3 vec = mainCamera.WorldToViewportPoint(transform.position);
        if (vec.x > 0 && vec.x < 1 && vec.y > 0 && vec.y < 1 && vec.z > mainCamera.nearClipPlane && vec.z < mainCamera.farClipPlane)
            Debug.Log("In sight!");
        else
            Debug.Log("Out of sight");
	}
}
