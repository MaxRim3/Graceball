using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundEffect_Controller : MonoBehaviour
{
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioSource audioSource;
    public AudioSource audioSourceSecondary;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Racket")
        {
           
            float collisionNum = collision.gameObject.GetComponent<Rigidbody>().velocity.x + collision.gameObject.GetComponent<Rigidbody>().velocity.y + collision.gameObject.GetComponent<Rigidbody>().velocity.z;
            //if (!audioSource.isPlaying)
            {
                //audioSource.volume = collisionNum / 10;
                //audioSourceSecondary.volume = collisionNum / 10;
                //audioSource.clip = audioClips[0];
                audioSource.Play();
            }
            //if (!audioSourceSecondary.isPlaying)
            {
                audioSourceSecondary.Play();
            }
        }
        else
        {
            audioSourceSecondary.volume = 0.3f;
            audioSourceSecondary.Play();
        }

    }
}
