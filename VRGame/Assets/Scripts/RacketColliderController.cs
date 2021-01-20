using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketColliderController : MonoBehaviour
{
    public GameObject racketToFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (racketToFollow)
        {
            this.transform.position = racketToFollow.transform.position;
            this.transform.rotation = racketToFollow.transform.rotation;
        }
    }
}
