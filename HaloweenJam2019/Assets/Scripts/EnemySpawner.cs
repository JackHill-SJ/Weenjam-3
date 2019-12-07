using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject zombie;
    public EnemyCounter enemyCounter;

    [Header("Size Constraints")]
    public Vector2 initializeRateDis;
    public Vector2 spawnDis;

    [Header("Bools")]
    public bool isSpawning;
    public bool hasSpawned;
    public bool isClose;
    public bool lightOn;

    [Header("Serialized Stuff")]
    [SerializeField] private float playerDistance;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnRate;
    [SerializeField] private Vector2 moduloSpawnPoint;



    private void DistanceUpdate()
    {
        playerDistance = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - transform.position.x, 2) + Mathf.Pow(player.transform.position.y - transform.position.y, 2) + Mathf.Pow(player.transform.position.z - transform.position.z, 2));
        if(playerDistance < initializeRateDis.y)
        {
            isSpawning = true;
            if(playerDistance < initializeRateDis.x)
            {
                isClose = true;
            }
            else
            {
                isClose = false;
            }
            Invoke("SpawnRateCalc", .5f);
        }
        else
        {
            isSpawning = false;
        }

        Invoke("DistanceUpdate", 1f);
    }
    private void SpawnRateCalc()
    {
        spawnRate = (playerDistance * spawnInterval) / (2 * initializeRateDis.y) + (spawnInterval / 2);
    }
    public void lightUpdate()
    {
        lightOn = !lightOn;
    }

    void Start()
    {
        playerDistance = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - transform.position.x , 2) + Mathf.Pow(player.transform.position.y - transform.position.y, 2) + Mathf.Pow(player.transform.position.z - transform.position.z, 2));

        isSpawning = false;
        hasSpawned = false;
        spawnRate = spawnInterval;

        Invoke("DistanceUpdate", 1f);
    }
    void Update()
    {
        if(isSpawning && enemyCounter.maxZombie > enemyCounter.zombieCount && !lightOn)
        {
            StartCoroutine("Spawning");
        }
    }

    IEnumerator Spawning()
    {
        if(!hasSpawned)
        {
            hasSpawned = true;
            GameObject test;
            if (!isClose)
            {
                test = Instantiate(zombie, transform.position + new Vector3(Random.Range(-spawnDis.x, spawnDis.x), 0f, Random.Range(-spawnDis.y, spawnDis.y)), transform.rotation);
            }
            else
            {
                moduloSpawnPoint = new Vector2(Random.Range(1, 10) % 2, Random.Range(1, 10) % 2);
                if(moduloSpawnPoint.x == 0)
                {
                    if(moduloSpawnPoint.y == 0)
                    {
                        test = Instantiate(zombie, transform.position + new Vector3(Random.Range(-spawnDis.x * 2, -spawnDis.x), 0f, Random.Range(-spawnDis.y * 2, -spawnDis.y)), transform.rotation);
                    }
                    else
                    {
                        test = Instantiate(zombie, transform.position + new Vector3(Random.Range(-spawnDis.x * 2, -spawnDis.x), 0f, Random.Range(spawnDis.y, spawnDis.y * 2)), transform.rotation);
                    }
                }
                else
                {
                    if (moduloSpawnPoint.y == 0)
                    {
                        test = Instantiate(zombie, transform.position + new Vector3(Random.Range(spawnDis.x, spawnDis.x * 2), 0f, Random.Range(-spawnDis.y * 2, -spawnDis.y)), transform.rotation);
                    }
                    else
                    {
                        test = Instantiate(zombie, transform.position + new Vector3(Random.Range(spawnDis.x, spawnDis.x * 2), 0f, Random.Range(spawnDis.y, spawnDis.y * 2)), transform.rotation);
                    }
                }
            }
            enemyCounter.zombieCount += 1;
            test.GetComponent<EnemyHealth>().enemyCounter = enemyCounter;
            test.GetComponent<EnemyFollowing>().target = player;
            test.transform.localScale *= Random.Range(.8f, 1.2f);

            yield return new WaitForSeconds(spawnRate);
            hasSpawned = false;
        }
    }
}