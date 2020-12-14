using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchController : MonoBehaviour
{
    public GestureSO fistGesture;
    public GestureSO palmGesture;
    public UnityEvent onGrab;
    public ThrowEvent onThrow;
    public float timeBetweenRecognition = .5f;
    public float recognitionThreshold = .05f;
    public bool waitForHighConfidenceData = true;
    public int curIdx = 0;
    public int methodIdx = 0;

    private Camera mainCamera;
    private bool isTriggered = false;
    private OVRSkeleton skeleton;
    private List<OVRBone> fingerBones;
    private OVRHand hand;
    private MySightItem mySightItem;
    private MyDistItem myDistItem;
    private MyGestureItem myGestureItem;

    void Start()
    {
        Debug.Log("Controller Start!!!");
        if (!mainCamera)
            mainCamera = Camera.main;
        mySightItem = new MySightItem();
        mySightItem.Init(mainCamera);
        myDistItem = new MyDistItem();
        myDistItem.Init(mainCamera);
        myGestureItem = new MyGestureItem();
        LoadSkeleton();
        myGestureItem.Init(fistGesture, palmGesture, timeBetweenRecognition, recognitionThreshold, waitForHighConfidenceData);
    }

    void Update()
    {
        // -1: no gesture, 1: throw, 2: grab 
        int flag = -1;
        switch (methodIdx)
        {
            // sight
            case 0:
                if (mySightItem.Triggered(transform))
                    flag = 1;
                else
                    flag = 2;
                break;
            // dist
            case 1:
                if (myDistItem.Triggered(transform))
                    flag = 1;
                else
                    flag = 2;
                break;
            // gesture
            case 2:
                if (mySightItem.Triggered(transform) && myDistItem.Triggered(transform))
                    flag = myGestureItem.Triggered(skeleton, fingerBones, hand);
                else
                    flag = -1;
                break;
            default:
                break;
        }
        if (flag == 1)
        {
            if (isTriggered)
            {
                isTriggered = false;
                Debug.Log("Throw!");
                onThrow.Invoke(curIdx);
            }
        }
        else if (flag == 2)
        {
            if (!isTriggered)
            {
                isTriggered = true;
                Debug.Log("Grab!");
                onGrab.Invoke();
            }
        }
    }

    void LoadSkeleton()
    {
        OVRSkeleton[] skeletons = FindObjectsOfType<OVRSkeleton>();
        foreach (OVRSkeleton _skeleton in skeletons)
        {
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
