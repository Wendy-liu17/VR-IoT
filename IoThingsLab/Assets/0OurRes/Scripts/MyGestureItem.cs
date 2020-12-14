using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyGestureItem
{
    private GestureSO fistGesture;
    private GestureSO palmGesture;
    private float timeBetweenRecognition;
    private float recognitionThreshold;
    private bool waitForHighConfidenceData;

    private float lastRecognition;
    private GestureSO prevGesture, curGesture;

    public void Init(GestureSO gesture1, GestureSO gesture2, float timeBetween, float threshold, bool wait)
    {
        fistGesture = gesture1;
        palmGesture = gesture2;
        timeBetweenRecognition = timeBetween;
        recognitionThreshold = threshold;
        waitForHighConfidenceData = wait;
        lastRecognition = timeBetweenRecognition;
        prevGesture = null;
        curGesture = null;
    }

    // -1: no gesture, 1: throw, 2: grab
    public int Triggered(OVRSkeleton skeleton, List<OVRBone> fingerBones, OVRHand hand)
    {
        if (fistGesture == null || palmGesture == null)
            return -1;
        lastRecognition += Time.deltaTime;
        if (lastRecognition < timeBetweenRecognition)
            return -1;

        curGesture = CheckRecognition(skeleton, fingerBones, hand);
        if (curGesture != null)
        {
            if (curGesture.name == palmGesture.name)
            {
                if (prevGesture == fistGesture && lastRecognition < 5 * timeBetweenRecognition)
                    return 1;
            }
            else
            {
                if (prevGesture == palmGesture && lastRecognition < 5 * timeBetweenRecognition)
                    return 2;
            }
            prevGesture = curGesture;
            lastRecognition = 0f;
        }
        return -1;
    }

    GestureSO CheckRecognition(OVRSkeleton skeleton, List<OVRBone> fingerBones, OVRHand hand)
    {
        // if (fingerBones.Count == 0)
        //     LoadSkeleton();
        if (fingerBones.Count == 0)
            return null;
        if (!hand.IsTracked)
            return null;
        if (!hand.IsDataHighConfidence && waitForHighConfidenceData)
            return null;

        // fist gesture
        float sumDistance = 0;
        bool isDiscarded = false;
        for (int i = 0; i < fingerBones.Count; i++)
        {
            Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
            float distance = Vector3.Distance(fistGesture.fingerPositions[i], currentData);
            if (distance > recognitionThreshold)
            {
                isDiscarded = true;
                break;
            }
            sumDistance = distance;
        }
        if (!isDiscarded)
        {
            Debug.Log("Gesture recognized with sum distance of: " + sumDistance);
            return fistGesture;
        }

        // palm gesture
        sumDistance = 0;
        isDiscarded = false;
        for (int i = 0; i < fingerBones.Count; i++)
        {
            Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
            float distance = Vector3.Distance(palmGesture.fingerPositions[i], currentData);
            if (distance > recognitionThreshold)
            {
                isDiscarded = true;
                break;
            }
            sumDistance = distance;
        }
        if (!isDiscarded)
        {
            Debug.Log("Gesture recognized with sum distance of: " + sumDistance);
            return palmGesture;
        }

        return null;
    }
}
