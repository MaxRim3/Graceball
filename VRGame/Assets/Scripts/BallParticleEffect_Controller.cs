using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallParticleEffect_Controller : MonoBehaviour
{
    public GameObject particleSystemPrefab;
    public bool hit;

    void OnCollisionEnter(Collision collision)
    {
        float particleScale = collision.transform.gameObject.GetComponent<Rigidbody>().velocity.x + collision.transform.gameObject.GetComponent<Rigidbody>().velocity.y + collision.transform.gameObject.GetComponent<Rigidbody>().velocity.z;
        ContactPoint contact = collision.contacts[0];
        
        GameObject hitParticle = Instantiate(particleSystemPrefab, contact.point, Quaternion.identity) as GameObject;
        hitParticle.transform.localScale = new Vector3(particleScale / 2, particleScale / 2, particleScale / 2);

    }
}
