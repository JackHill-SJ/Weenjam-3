using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrickle : MonoBehaviour
{
    public List<Transform> nodes;
    public GameObject player;
    public GameObject zombie;
    public EnemyCounter enemyCounter;

    [Header("Size Constraints")]
    [Tooltip("min and max horizontal plane spawn distances")]
    public Vector2 initializeRateDis;
    public Vector2 spawnDis;

    [Header("Bools")]
    public List<bool> isSpawning;
    public List<bool> hasSpawned;

    [Header("Serialized Stuff")]
    [SerializeField] public List<float> playerDistance;
    [SerializeField] private float spawnRate;

    private void DistanceUpdate()
    {
        for (int i = 0; i < nodes.Count; i += 1)
        {
            playerDistance[i] = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - nodes[i].position.x, 2) + Mathf.Pow(player.transform.position.y - nodes[i].position.y, 2) + Mathf.Pow(player.transform.position.z - nodes[i].position.z, 2));
            if (playerDistance[i] > initializeRateDis.x && playerDistance[i] < initializeRateDis.y)
            {
                isSpawning[i] = true;
            }
            else
            {
                isSpawning[i] = false;
            }
        }
        Invoke("DistanceUpdate", 1f);
    }

    void Start()
    {
        for (int i = 0; i < nodes.Count; i += 1)
        {
            playerDistance[i] = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - nodes[i].position.x, 2) + Mathf.Pow(player.transform.position.y - nodes[i].position.y, 2) + Mathf.Pow(player.transform.position.z - nodes[i].position.z, 2));
            isSpawning[i] = false;
            hasSpawned[i] = false;
        }

        Invoke("DistanceUpdate", 1f);
    }
    void Update()
    {
        for (int i = 0; i < nodes.Count; i += 1)
        {
            if (isSpawning[i] && enemyCounter.maxZombie > enemyCounter.zombieCount)
            {
                StartCoroutine("Spawning", i);
            }
        }
    }
    IEnumerator Spawning(int i)
    {
        print("Spawning");
        if (!hasSpawned[i])
        {
            hasSpawned[i] = true;
            yield return new WaitForSeconds(Random.Range(0, spawnRate));

            enemyCounter.zombieCount += 1;
            GameObject test = Instantiate(zombie, nodes[i].position + new Vector3(Random.Range(-spawnDis.x, spawnDis.x), 0f, Random.Range(-spawnDis.y, spawnDis.y)), transform.rotation);

            test.GetComponent<EnemyHealth>().enemyCounter = enemyCounter;
            test.GetComponent<EnemyFollowing>().target = player;

            yield return new WaitForSeconds(spawnRate * (nodes.Count - 2));
            hasSpawned[i] = false;
        }
    }
    public void ClearNodes()
    {
        foreach (Transform node in nodes)
        {
            node.gameObject.GetComponent<SpawnerNode>().DeleteNode();
        }
        isSpawning.Clear();
        hasSpawned.Clear();
        playerDistance.Clear();
    }
}