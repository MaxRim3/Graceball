using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;

    [SerializeField] GameObject ballSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, ballSpawnPoint.transform.position, ballSpawnPoint.transform.rotation) as GameObject;
    }
}
