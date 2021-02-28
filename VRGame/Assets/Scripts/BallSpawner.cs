namespace Photon.Pun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Collections;


    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] GameObject ballPrefab;

        [SerializeField] GameObject ballSpawnPoint;
        private readonly OVRInput.Button thumbOne = OVRInput.Button.One;
        private readonly OVRInput.Button thumbTwo = OVRInput.Button.Two;
        public OVRInput.Controller leftController;
        public OVRInput.Controller rightController;
        public bool ballJustSpawned = false;
        public GameObject[] balls;

        public AudioSource crowdCheerAudioSource;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(spawnBall());
        }

        // Update is called once per frame
        void Update()
        {
            
            if (OVRInput.Get(thumbTwo, leftController))
            {
                if (!ballJustSpawned)
                {
                    StartCoroutine(spawnBall());
                    ballJustSpawned = true;
                }
            }

            if (OVRInput.Get(thumbTwo, rightController))
            {
                if (!ballJustSpawned)
                {
                    StartCoroutine(spawnBall());
                    ballJustSpawned = true;
                }
            }

            if (OVRInput.Get(thumbOne, leftController))
            {
                clearBalls();
            }

            if (OVRInput.Get(thumbOne, rightController))
            {

                clearBalls();
            }
        }
    

        public IEnumerator spawnBall()
        {
            GameObject ball = PhotonNetwork.Instantiate(ballPrefab.name, ballSpawnPoint.transform.position, ballSpawnPoint.transform.rotation) as GameObject;
            ball.GetComponent<BallSoundEffect_Controller>().crowdAudioSource = crowdCheerAudioSource;
            yield return new WaitForSeconds(5);
            ballJustSpawned = false;
        }

        public void clearBalls()
        {
            
            balls = GameObject.FindGameObjectsWithTag("Respawn");
            for (int i = 0; i < balls.Length; i++)
            {
                Destroy(balls[i].gameObject);
            }
        }


    }
}
