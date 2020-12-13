using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyDistItem : MonoBehaviour {
    public UnityEvent event1;
    public UnityEvent event2;

    private Camera mainCamera;
    private bool isTriggered = false;

    void Start() {
        Debug.Log("Dist Start!!!");
		if (!mainCamera)
			mainCamera = Camera.main;
    }

    void Update() {
        if (transform.position[0] - mainCamera.transform.position[0] > 0 && transform.position[0] - mainCamera.transform.position[0] < 5 && System.Math.Abs(transform.position[2] - mainCamera.transform.position[2]) < 2.2) {
            if (isTriggered) {
                isTriggered = false;
                Debug.Log("In dist!");
                event1.Invoke();
            }
        }
        else {
            if (!isTriggered) {
                isTriggered = true;
                Debug.Log("Out of dist!");
                event2.Invoke();
            }
        }
    }
}
