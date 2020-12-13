using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyGestureItem : MonoBehaviour {
    [Tooltip("Drag the gesture you wish to track here")]
    public GestureSO fistGesture;
    public GestureSO palmGesture;
    [Tooltip("Event that runs when we recognize a gesture")]
    public UnityEvent OnGrab;
    public UnityEvent OnThrow;
    [SerializeField]
    [Tooltip("Is this gesture recognizer active right now?")]
    public bool shouldRecognize = true;
    [Tooltip("Minimum required wait time before firing another recogintion event")]
    public float timeBetweenRecognition = 1f;
    [Tooltip("Raising this value will result in more recognition at the cost of precision and accuracy")]
    public float recognitionThreshold = .05f;
    [Tooltip("Enabling this will only trigger recognition when we are certain we can see hands")]
    public bool waitForHighConfidenceData = true;

    private OVRSkeleton skeleton;
    private List<OVRBone> fingerBones;
    private float lastRecognition;
    private GestureSO prevGesture, curGesture;
    private OVRHand hand;

    private void Start() {
        lastRecognition = timeBetweenRecognition;
        prevGesture = null;
        curGesture = null;
        LoadSkeleton();
    }

    private void Update() {
        if (fistGesture == null || palmGesture == null) return;

        lastRecognition += Time.deltaTime;

        if (lastRecognition < timeBetweenRecognition) return;

        curGesture = CheckRecognition();
        if (curGesture != null) {
            Debug.Log("Recognized Gesture" + curGesture.name);
            if (curGesture.name == fistGesture.name) {
                Debug.Log("===============recognized fist===============\n");
                if (prevGesture == palmGesture && lastRecognition < 5 * timeBetweenRecognition)
                    OnGrab?.Invoke();
            }
            else {
                Debug.Log("===============recognized palm===============\n");
                if (prevGesture == fistGesture && lastRecognition < 5 * timeBetweenRecognition)
                    OnThrow?.Invoke();
            }
            prevGesture = curGesture;
            lastRecognition = 0f;
            return;
        }
    }

    GestureSO CheckRecognition() {
        if (fingerBones.Count == 0) LoadSkeleton();

        if (fingerBones.Count == 0) return null;

        if (!hand.IsTracked) return null;

        if (!hand.IsDataHighConfidence && waitForHighConfidenceData) return null;

        // fist gesture?
        float sumDistance = 0;
        bool isDiscarded = false;
        for (int i = 0; i < fingerBones.Count; i++) {
            Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
            float distance = Vector3.Distance(fistGesture.fingerPositions[i], currentData);
            if (distance > recognitionThreshold) {
                isDiscarded = true;
                break;
            }
            sumDistance = distance;
        }
        if (!isDiscarded) {
            Debug.Log("Gesture recognized with sum distance of: " + sumDistance);
            return fistGesture;
        }

        // palm gesture
        sumDistance = 0;
        isDiscarded = false;
        for (int i = 0; i < fingerBones.Count; i++) {
            Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
            float distance = Vector3.Distance(palmGesture.fingerPositions[i], currentData);
            if (distance > recognitionThreshold) {
                isDiscarded = true;
                break;
            }
            sumDistance = distance;
        }
        if (!isDiscarded) {
            Debug.Log("Gesture recognized with sum distance of: " + sumDistance);
            return palmGesture;
        }

        // no gesture
        return null;
    }

    void LoadSkeleton() {
        OVRSkeleton[] skeletons = FindObjectsOfType<OVRSkeleton>();
        foreach (OVRSkeleton _skeleton in skeletons) {
            if (fistGesture.hand == GestureHand.RightHand && _skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandRight)
                skeleton = _skeleton;
            else if (fistGesture.hand == GestureHand.LeftHand && _skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandLeft)
                skeleton = _skeleton;
        }

        fingerBones = new List<OVRBone>(skeleton.Bones);
        hand = skeleton.gameObject.GetComponent<OVRHand>();

        if (fingerBones == null || skeleton == null) Debug.LogWarning("Failed to find skeleton for " + fistGesture.hand + ". Do you have an OVRHandPrefab in the scene and setup?");
    }
}
