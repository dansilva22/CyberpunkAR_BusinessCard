using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLane : MonoBehaviour
{
    public Transform spawn;
    public float freq;
    public float variation = 1.0f;
    public Transform target; 
    public List<GameObject> vehicles;
    int vehicleCount;

    public bool spawnActive = true;
    
  

    public IEnumerator Spawner()
    {
        vehicleCount = vehicles.Count;
        Debug.Log(vehicleCount + " Vehicles in list");
        while(spawnActive)
        {
            Debug.Log("spawning new vehicle");
            //determine how long to wait before next spawn
            float wait = Random.Range(1.0f,freq);
            //which vehicle?
            int random = Random.Range(0,vehicleCount);
            float var = Random.Range(0.0f,variation);

            Vector3 spawnAdjust = spawn.position;
            Vector3 targetAdjust = target.position;

            //spread the vehicle spawn pos
            spawnAdjust.x += var;
            spawnAdjust.y += var;

            targetAdjust.x += var;
            targetAdjust.y += var;

            


            //spawn random vehicle, populate it's values.
            GameObject vehicle = Instantiate(vehicles[random], spawnAdjust, spawn.rotation);
            vehicle.GetComponent<VehicleMover>().target = targetAdjust;

            //wait til next spawn
            yield return new WaitForSeconds(wait);
        }
    } 
}
