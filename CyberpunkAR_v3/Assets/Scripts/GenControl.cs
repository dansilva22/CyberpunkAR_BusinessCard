using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GenControl : DefaultTrackableEventHandler 
{
    public List<GameObject> buildings = new List<GameObject>();
    
    // Start is called before the first frame update
    

    protected override void  OnTrackingFound()
    {
        Regenerate();
    }



    public void Regenerate()
    {
        //find all the buildings in the scene 
        if(buildings.Count == 0){
            foreach(GameObject building in GameObject.FindGameObjectsWithTag("Building"))
            {
                buildings.Add(building);
            }
        }
        

        Debug.Log("There are " + buildings.Count + " buildings.");
        

        foreach(GameObject building in buildings)
        {
            BuildingGenerator buildGen = building.GetComponent<BuildingGenerator>();
            if(buildGen.temp.Count > 0)
            {
                for(int i = buildGen.temp.Count -1; i >= 0; i--)
                {
                    GameObject t = buildGen.temp[i];
                    buildGen.temp.RemoveAt(i);
                    Destroy(t);

                }
            }
            
            

            buildGen.StartCoroutine("Generate");
        }



    }
}
