using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    public GestureSO fistGesture;
    public GestureSO palmGesture;
    public UnityEvent onGrab;
    public ThrowEvent onThrow;

    // public Button methodButton1;
    // public Button methodButton2;
    // public Button methodButton3;
    // public Button methodButton4;
    // public Button switchButton;

    public float timeBetweenRecognition = 0.5f;
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

        if (curIdx == 1)
            isTriggered = true;
        if (curIdx == 2)
            isTriggered = false;
        mySightItem = new MySightItem();
        mySightItem.Init(mainCamera);
        myDistItem = new MyDistItem();
        myDistItem.Init(mainCamera);
        myGestureItem = new MyGestureItem();
        LoadSkeleton();
        myGestureItem.Init(fistGesture, palmGesture, timeBetweenRecognition, recognitionThreshold, waitForHighConfidenceData);

        // methodButton1.onClick.AddListener(func1);
        // methodButton2.onClick.AddListener(func2);
        // methodButton3.onClick.AddListener(func3);
        // methodButton4.onClick.AddListener(func4);
        // switchButton.onClick.AddListener(switchFunc);
    }

    public void func1()
    {
        methodIdx = 1;
        Debug.Log("Button 1 clicked!");
    }

    public void func2()
    {
        methodIdx = 2;
        Debug.Log("Button 2 clicked!");
    }

    public void func3()
    {
        methodIdx = 3;
        Debug.Log("Button 3 clicked!");
    }

    public void func4()
    {
        methodIdx = 4;
        Debug.Log("Button 4 clicked!");
    }

    public void switchFunc() {
        if (methodIdx == 4) {
            if (isTriggered)
            {
                isTriggered = false;
                Debug.Log("Throw!");
                onThrow.Invoke(curIdx);
            }
            else
            {
                isTriggered = true;
                Debug.Log("Grab!");
                onGrab.Invoke();
            }
        }
    }

    void Update()
    {
        // -1: no gesture, 1: throw, 2: grab 
        int flag = -1;
        switch (methodIdx)
        {
            // sight
            case 1:
                if (mySightItem.Triggered(transform))
                    flag = 1;
                else
                    flag = 2;
                break;
            // dist
            case 2:
                if (myDistItem.Triggered(transform))
                    flag = 1;
                else
                    flag = 2;
                break;
            // gesture
            case 3:
                if (mySightItem.Triggered(transform))
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
