using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDistItem : MonoBehaviour
{
    private Camera mainCamera;	

    void Start() {
        Debug.Log("Dist Start!!!");
		if (!mainCamera)
			mainCamera = Camera.main;
    }

    void Update() {
        if (transform.position[0] - mainCamera.transform.position[0] > 0 && transform.position[0] - mainCamera.transform.position[0] < 5 && System.Math.Abs(transform.position[2] - mainCamera.transform.position[2]) < 2.2)
            Debug.Log("In dist!");
        else
            Debug.Log("Out of dist!");
    }
}
