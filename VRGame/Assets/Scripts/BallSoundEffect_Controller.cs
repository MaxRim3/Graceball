using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundEffect_Controller : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioClip[] goalAudioClips;
    public AudioSource crowdAudioSource;
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
        else if (collision.gameObject.tag == "RedGoal" || collision.gameObject.tag == "BlueGoal")
        {
            AudioClip clip = goalAudioClips[Random.Range(0, goalAudioClips.Length)];
            crowdAudioSource.PlayOneShot(clip);
        }
        else
        {
            audioSourceSecondary.volume = 0.3f;
            audioSourceSecondary.Play();
        }

    }
}
