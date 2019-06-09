using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GenControl : MonoBehaviour, ITrackableEventHandler 
{
    public List<GameObject> buildings = new List<GameObject>();
    
    // Start is called before the first frame update
    

 /* protected override void  OnTrackingFound()
    {
        Regenerate();
    }*/

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    void OnTrackingFound()
    {
        Debug.Log("Tracking found");
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;

        Regenerate();
    }

    void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;

        buildings.Clear();
    }




    public void Regenerate()
    {
        Debug.Log("Regenerating buildings");
        //find all the buildings in the scene 
       // if(buildings.Count == 0){
            foreach(GameObject building in GameObject.FindGameObjectsWithTag("Building"))
            {
                buildings.Add(building);
            }
      //  }
        

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
