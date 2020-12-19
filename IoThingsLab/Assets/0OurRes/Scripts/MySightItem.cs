using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MySightItem
{
    private Camera mainCamera;
    private float gazeTime;

    private bool hasSight;
    private float duration;

    public void Init(Camera camera, float time)
    {
        Debug.Log("Sight Start!!!");
        mainCamera = camera;
        gazeTime = time;
    }

    public bool Triggered(Transform transform)
    {
        Ray ray = mainCamera.ScreenPointToRay(transform.position);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit, transform.position.sqrMagnitude);
        if (isCollider)
        {
            if (!hit.collider.gameObject.name.Equals(transform.gameObject.name))
            {
                if (hit.collider.gameObject.name.Equals("Cube (2)") && transform.gameObject.name.Contains("1"))
                {
                    if (hasSight)
                    {
                        duration += Time.deltaTime;
                        if (duration >= gazeTime) {
                            hasSight = false;
                            duration = 0;
                            return false;
                        }
                        return true;
                    }
                    return false;
                }

                // room in which tv is on
                var currentRoom = transform.parent.transform.parent.transform.parent;

                // room in which obstacle is found
                var parentRoom = hit.collider.gameObject;
                while (!parentRoom.name.Contains("room"))
                {
                    parentRoom = parentRoom.transform.parent.gameObject;
                }

                if (!parentRoom.name.Equals(currentRoom.name))
                {
                    if (hasSight)
                    {
                        duration += Time.deltaTime;
                        if (duration >= gazeTime) {
                            hasSight = false;
                            duration = 0;
                            return false;
                        }
                        return true;
                    }
                    return false;
                }
            }
        }

        Vector3 vec = mainCamera.WorldToViewportPoint(transform.position);
        if (vec.x > 0 && vec.x < 1 && vec.y > 0 && vec.y < 1 && vec.z > 0)
        {
            if (!hasSight)
            {
                duration += Time.deltaTime;
                if (duration >= gazeTime) {
                    hasSight = true;
                    duration = 0;
                    return true;
                }
                return false;
            }
            return true;
        }
        if (hasSight)
        {
            duration += Time.deltaTime;
            if (duration >= gazeTime) {
                hasSight = false;
                duration = 0;
                return false;
            }
            return true;
        }
        return false;
    }
}
