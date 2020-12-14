using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MySightItem
{
    private Camera mainCamera;

    public void Init(Camera camera) {
        Debug.Log("Sight Start!!!");
		mainCamera = camera;
    }

    public bool Triggered(Transform transform)
    {        
        Ray ray = mainCamera.ScreenPointToRay(transform.position);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit, transform.position.sqrMagnitude );
        if(isCollider)
        {
            Debug.Log("-----------------------------"+hit.collider.gameObject.name+"||actual is:"+ transform.gameObject.name+"-------------------");
            if(!hit.collider.gameObject.name.Equals(transform.gameObject.name))
            {
                if (hit.collider.gameObject.name.Equals("Cube (2)")&&transform.gameObject.name.Contains("1"))
                {
                    Debug.Log("=========obstacle sight!========");
                    return false;
                }

                // room in which tv is on
                var currentRoom = transform.parent.transform.parent.transform.parent;
                Debug.Log("++++++++++++++++current Room:+++++++++++"+currentRoom.name);
                               

                // room in which obstacle is found
                var parentRoom = hit.collider.gameObject;
                while(!parentRoom.name.Contains("room"))
                {
                    parentRoom = parentRoom.transform.parent.gameObject;
                }

                Debug.Log("++++++++++++++++obstacle Room:+++++++++++" + parentRoom.name);                
                if(!parentRoom.name.Equals(currentRoom.name))
                {
                    Debug.Log("=========obstacle sight!========");
                    return false;
                }               
            }
            Debug.Log("=========direct sight!========");
        }

        Vector3 vec = mainCamera.WorldToViewportPoint(transform.position);
        if (vec.x > 0 && vec.x < 1 && vec.y > 0 && vec.y < 1 && vec.z>0)//vec.z > mainCamera.nearClipPlane && vec.z < mainCamera.farClipPlane)
        {
            Debug.Log("---------------In sight!"+ transform.gameObject.name);
            return true;
        }
        Debug.Log("Out of sight!");
        return false;
    }
}
