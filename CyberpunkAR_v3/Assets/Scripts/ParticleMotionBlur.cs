using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMotionBlur : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystemRenderer renderer = GetComponent<ParticleSystemRenderer>();
        renderer.motionVectorGenerationMode = MotionVectorGenerationMode.Camera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
