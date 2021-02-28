using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float maximumSpeed = 20;
    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transferOwnership();
        float speed = Vector3.Magnitude(rigidbody.velocity);  // test current object speed

        if (speed > maximumSpeed)

        {
            float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease

            Vector3 normalisedVelocity = rigidbody.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rigidbody.AddForce(-brakeVelocity);  // apply opposing brake force
        }
    }

    public void transferOwnership()
    {
        pv.TransferOwnership(PhotonNetwork.LocalPlayer);
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Racket")
    //    {
    //        pv.TransferOwnership(collision.gameObject.GetComponent<PhotonView>.);
    //    }
    //}
}
