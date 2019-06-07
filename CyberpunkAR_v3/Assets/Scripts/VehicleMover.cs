using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMover : MonoBehaviour
{
    float t;
    public Vector3 startPosition;
    public Vector3 target;
    public float timeToReachTarget;
     void Start()
     {
            // startPosition = target = transform.position;
            startPosition = transform.position;
            
     }
     void Update() 
     {
         if(target != null)
         {
             t += Time.deltaTime/timeToReachTarget;
             transform.position = Vector3.Lerp(startPosition, target, t);
         }
         if(transform.position ==target)
         {
             Destroy(this.gameObject);
         }
             
     }
  //public void SetDestination(Transform destination, float time)
     //{
           // t = 0;
           // startPosition = transform.position;
           // timeToReachTarget = time;
           // target = destination; 

    // }
}
