using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyDistItem : MySensorItem
{
    private Camera mainCamera;

    void Start() {
        Debug.Log("Dist Start!!!");
		if (!mainCamera)
			mainCamera = Camera.main;
        UnityAction triggeredAction = new UnityAction(func1);
        UnityAction untriggeredAction = new UnityAction(func2);
        setTriggered(triggeredAction);
        setUnTriggered(untriggeredAction);
    }

    void Update() {
        if (transform.position[0] - mainCamera.transform.position[0] > 0 && transform.position[0] - mainCamera.transform.position[0] < 5 && System.Math.Abs(transform.position[2] - mainCamera.transform.position[2]) < 2.2)
            SensorTrigger();
        else
            SensorUntrigger();
    }

    void func1() {
        Debug.Log("In dist!");
    }

    void func2() {
        Debug.Log("Out of dist!");
    }
}
