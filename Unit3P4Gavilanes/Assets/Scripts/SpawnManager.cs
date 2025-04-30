using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclesPrefab;
    private int obstacleIndex;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay, repeatRate;

    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);

        playerControllerScript = GameObject.Find("Steve").GetComponent<PlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        obstacleIndex = Random.Range(0,4);

        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclesPrefab[obstacleIndex], spawnPos, obstaclesPrefab[obstacleIndex].transform.rotation);
        }

        // change the repeat rate after each obstacle spawns

        repeatRate = Random.Range(1f, 2.5f);
        startDelay = Random.Range(1f, 2.5f);
        CancelInvoke();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }
}
