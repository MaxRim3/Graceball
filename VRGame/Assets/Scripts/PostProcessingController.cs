using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    public PostProcessProfile ppProfile;
    public Vignette v;
    public DepthOfField dof;
    public float dofAmount = 0f;

    public float min = 0f;
    public float max = 5f;
    public float velocity;
    public float normalizedVelocity;
    public bool blur;
    // Start is called before the first frame update
    void Start()
    {
        v = ppProfile.GetSetting<Vignette>();
        dof = ppProfile.GetSetting<DepthOfField>();
        dofAmount = dof.focusDistance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        normalizedVelocity = GetComponent<Rigidbody>().velocity.magnitude;
        normalizedVelocity = (velocity - min) / (max - min);
        velocity = Mathf.Clamp(velocity, min, max);
        normalizedVelocity = Mathf.Clamp(normalizedVelocity, 0, 1);
        //v.intensity.Override(GetComponent<Rigidbody>().velocity.magnitude / 10);
        print(normalizedVelocity);
        dof.focusDistance.Override(dofAmount);
    }
}
