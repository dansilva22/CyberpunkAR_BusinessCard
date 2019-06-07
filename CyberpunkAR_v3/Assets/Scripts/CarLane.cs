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

    bool spawnActive = true;
    // Start is called before the first frame update
    void Start()
    {
        vehicleCount = vehicles.Count;
        Debug.Log(vehicleCount + " Vehicles in list");
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spawner()
    {
        while(spawnActive)
        {
            Debug.Log("spawning new vehicle");
            //determine how long to wait before next spawn
            float wait = Random.Range(1.0f,freq);
            int random = Random.Range(0,vehicleCount);
            float var = Random.Range(0.0f,variation);

            Vector3 spawnAdjust = spawn.position;
            Vector3 targetAdjust = target.position;
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
