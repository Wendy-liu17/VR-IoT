using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyEvent1:UnityEvent<int>{}
public class MyEvent2:UnityEvent<int,int>{}


public class MySensorItem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Action performed after sensor is triggered")]
    UnityEvent triggeredEvent;
    MyEvent1 triggeredEvent1;
    MyEvent2 triggeredEvent2;
    [SerializeField]
    [Tooltip("Action performed after sensor is triggered")]
    UnityEvent untriggeredEvent;
    MyEvent1 untriggeredEvent1;
    MyEvent2 untriggeredEvent2;


    bool isSensorTriggered = false;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    public void setTriggered(UnityAction action) {
        triggeredEvent.AddListener(action);
    }

    public void setTriggered(UnityAction<int> action) {
        triggeredEvent1.AddListener(action);
    }

    public void setTriggered(UnityAction<int,int> action) {
        triggeredEvent2.AddListener(action);
    }

    public void setUnTriggered(UnityAction action) {
        untriggeredEvent.AddListener(action);
    }

    public void setUnTriggered(UnityAction<int> action) {
        untriggeredEvent1.AddListener(action);
    }

    public void setUnTriggered(UnityAction<int,int> action) {
        untriggeredEvent2.AddListener(action);
    }

    void SensorTriggered() {
        isSensorTriggered = true;
        triggeredEvent?.Invoke();
    }

    void SensorTriggered(int a) {
        isSensorTriggered = true;
        triggeredEvent1?.Invoke(a);
    }

    void SensorTriggered(int a,int b) {
        isSensorTriggered = true;
        triggeredEvent2?.Invoke(a,b);
    }

    void SensorUntriggered() {
        isSensorTriggered = false;
        untriggeredEvent?.Invoke();
    }

    void SensorUntriggered(int a) {
        isSensorTriggered = false;
        untriggeredEvent1?.Invoke(a);
    }

    void SensorUntriggered(int a,int b) {
        isSensorTriggered = false;
        untriggeredEvent2?.Invoke(a,b);
    }

    public void SensorTrigger() {
        if (!isSensorTriggered)
            SensorTriggered();
    }

    public void SensorTrigger(int a) {
        if (!isSensorTriggered)
            SensorTriggered(a);
    }

    public void SensorTrigger(int a,int b) {
        if (!isSensorTriggered)
            SensorTriggered(a,b);
    }

    public void SensorUntrigger() {
        if (isSensorTriggered)
            SensorUntriggered();
    }

    public void SensorUntrigger(int a) {
        if (isSensorTriggered)
            SensorUntriggered(a);
    }

    public void SensorUntrigger(int a,int b) {
        if (isSensorTriggered)
            SensorUntriggered(a,b);
    }
}
