using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MySensorItem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Action performed after sensor is triggered")]
    UnityEvent triggeredEvent;
    [SerializeField]
    [Tooltip("Action performed after sensor is triggered")]
    UnityEvent untriggeredEvent;

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

    public void setUnTriggered(UnityAction action) {
        untriggeredEvent.AddListener(action);
    }

    void SensorTriggered() {
        isSensorTriggered = true;
        triggeredEvent?.Invoke();
    }

    void SensorUntriggered() {
        isSensorTriggered = false;
        untriggeredEvent?.Invoke();
    }

    public void SensorTrigger() {
        if (!isSensorTriggered)
            SensorTriggered();
    }

    public void SensorUntrigger() {
        if (isSensorTriggered)
            SensorUntriggered();
    }
}
