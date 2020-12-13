// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// [System.Serializable]
// public struct Gesture
// {
//     // the name of the pose
//     public string name;
//     // the position data of the bones
//     public List<Vector3> fingerDatas;
//     // triggered event
//     public UnityEvent onRecognized;
// }

// public class GestureDetector : MonoBehaviour
// {
//     public float threshold = 0.05f;
//     public OVRSkeleton skeleton;
//     public List<Gesture> gestures;
//     public bool debugMode = true;
//     private List<OVRBone> fingerBones;
//     private Gesture previousGesture;

//     // Start is called before the first frame update
//     void Start()
//     {
//         Debug.Log("Start!!");
//         fingerBones = new List<OVRBone>(skeleton.Bones);
//         gestures = new List<Gesture>();
//         previousGesture = new Gesture();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // if (debugMode && Input.GetKeyDown(KeyCode.Space)) {
//         Save();
//         // }
//         Gesture currentGesture = Recognize();
//         bool hasRecognized = !currentGesture.Equals(new Gesture());
//         // check if new gesture
//         if (hasRecognized && !currentGesture.Equals(previousGesture))
//         {
//             Debug.Log("New Gesture Found : " + currentGesture.name);
//             previousGesture = currentGesture;
//             currentGesture.onRecognized.Invoke();
//         }
//     }

//     // create a new gesture
//     void Save()
//     {
//         Debug.Log("In save");
//         Gesture gesture = new Gesture();
//         gesture.name = "New Gesture";
//         List<Vector3> data = new List<Vector3>();
//         foreach (var bone in fingerBones)
//         {
//             // finger position relative to root
//             data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));

//         }
//         Debug.Log(data);
//         gesture.fingerDatas = data;
//         gestures.Add(gesture);
//     }

//     Gesture Recognize()
//     {
//         Gesture currentGesture = new Gesture();
//         float currentMin = Mathf.Infinity;
//         foreach (var gesture in gestures)
//         {
//             float sumDistance = 0;
//             bool isDiscarded = false;
//             for (int i = 0; i < fingerBones.Count; i++)
//             {
//                 Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
//                 float distance = Vector3.Distance(currentData, gesture.fingerDatas[i]);
//                 if (distance > threshold)
//                 {
//                     isDiscarded = true;
//                     break;
//                 }
//                 sumDistance += distance;
//             }
//             if (!isDiscarded && sumDistance < currentMin)
//             {
//                 currentMin = sumDistance;
//                 currentGesture = gesture;
//             }
//         }
//         return currentGesture;
//     }
// }
