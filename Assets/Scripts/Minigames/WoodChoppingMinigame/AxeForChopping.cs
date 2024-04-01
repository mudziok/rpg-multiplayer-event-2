using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeForChopping : MonoBehaviour
{
    bool hover = false;
    bool drag = false;

    Transform targetTree;
    float distance;
    Vector3 initPose;

    public void Init(TreeForChopping tree)
    {
        targetTree = tree.transform;
        initPose = transform.position;
    }



     void Update()
     {
         Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit[] allCasts = Physics.RaycastAll(camRay, 300);
         for(int i=0; i<allCasts.Length; i++)
         {
             if (allCasts[i].collider.gameObject == this.gameObject)
             {
                 hover = true;
                 break;
             }
         }

         if (hover)
         {
             if (Input.GetMouseButtonDown(0))
             {
                 drag = true;
                 Vector3 a = Camera.main.transform.position;
                 Vector3 b = targetTree.transform.position;
                 a.y = 0.0f;
                 b.y = 0.0f;
                 distance = Vector3.Distance(a,b);
             }
         }

         if (Input.GetMouseButtonUp(0))
         {
             drag = false;
         }

         if (drag)
         {
            //Ok distance jest dla k¹ta 0...
             float multipler = Mathf.Cos(Vector3.Angle(Camera.main.transform.forward, camRay.direction)*Mathf.Deg2Rad);
             Vector3 endPoint = camRay.origin + camRay.direction * (distance/multipler);
             transform.position = endPoint;
         }
         else
         {
             transform.Translate((initPose - transform.position) * 5.0f * Time.deltaTime, Space.World);
             transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 0.5f);
         }
     }
}
