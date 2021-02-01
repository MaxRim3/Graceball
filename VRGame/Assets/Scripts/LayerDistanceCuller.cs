using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDistanceCuller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[15] = 15;
        camera.layerCullDistances = distances;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
