using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public bool colliding;
    public Rigidbody guide;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        guide = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
        if (colliding)
        {
            guide.AddForce(-_rigidbody.velocity);
            print(_rigidbody.velocity);
            print("adding velocity");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
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
}
