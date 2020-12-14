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

    public Button button_method1;
    public Button button_method2;
    public Button button_method3;
    public Button button_method4;

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
        mySightItem = new MySightItem();
        mySightItem.Init(mainCamera);
        myDistItem = new MyDistItem();
        myDistItem.Init(mainCamera);
        myGestureItem = new MyGestureItem();
        LoadSkeleton();
        myGestureItem.Init(fistGesture, palmGesture, timeBetweenRecognition, recognitionThreshold, waitForHighConfidenceData);

        button_method1.onClick.AddListener(func1);
        button_method2.onClick.AddListener(func2);
        button_method3.onClick.AddListener(func3);
        button_method4.onClick.AddListener(func4);
    }

    void func1()
    {
        methodIdx = 1;
        Debug.Log("Button 1 clicked!");
    }

    void func2()
    {
        methodIdx = 2;
        Debug.Log("Button 2 clicked!");
    }

    void func3()
    {
        methodIdx = 3;
        Debug.Log("Button 3 clicked!");
    }

    void func4()
    {
        methodIdx = 4;
        Debug.Log("Button 4 clicked!");
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
