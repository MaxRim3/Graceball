using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketVelocityController : MonoBehaviour
{
    private BatCollider _batGuide;
    public Rigidbody guide;
    public GameObject guideTwo;
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    public bool colliding;

    [SerializeField]
    private float _sensitivity = 10f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        guide = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        guideTwo = GameObject.FindWithTag("PlayerChild");
        //this.gameObject.transform.parent = guideTwo.transform;

    }

    private void FixedUpdate()
    {
        Vector3 destination = _batGuide.transform.position;
        _rigidbody.transform.rotation = transform.rotation;

        _velocity = (destination - _rigidbody.transform.position) * _sensitivity;
        _velocity = _velocity + guide.velocity;

        if (colliding)
        {
            //guide.AddForce(-_velocity);
        }

    
    }

    public void SetFollowTarget(BatCollider batGuide)
    {
        _batGuide = batGuide;
    }

    public void SetGuide(Rigidbody guider)
    {
        guide = guider;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            colliding = true;
        }
        colliding = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            colliding = false;
        }
    }

}
