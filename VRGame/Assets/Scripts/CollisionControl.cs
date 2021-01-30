using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public bool colliding;
    public Rigidbody guide;
    public float dist;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        guide = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
        if (colliding)
        {
            if (CheckDirection())
            {
                float multiplier = 10;
                //Vector3 multipliedVelocity = new Vector3(-_rigidbody.velocity.x * multiplier, -_rigidbody.velocity.y * multiplier, -_rigidbody.velocity.z * multiplier);
                guide.AddForce(-_rigidbody.velocity);
                print(_rigidbody.velocity);
                print("adding velocity");
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        guide.velocity = new Vector3(0, 0, 0);
        if (collision.gameObject.tag == "Wall")
        {
            colliding = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            colliding = false;
        }
    }

    //True of object is moving away from guide
    public bool CheckDirection()
    {
        float distTemp = Vector3.Distance(guide.position, transform.position);
        if (distTemp < dist)
        {
            dist = distTemp;
            return false;
        }
        else if (distTemp > dist)
        {
            dist = distTemp;
            return true;
        }
        return false;
    }
}
