using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This is a script that allows preplaced spawner node to gain access to things in the spawnroom scene
public class SpawnerNode : MonoBehaviour
{
    private EnemyTrickle et;
    private void Start()
    {
        //Moves itself into the correct scene then adds itself to the trickle node list
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex(1));
        et = GameObject.FindGameObjectWithTag("EnemyTrickle").GetComponent<EnemyTrickle>();
        et.nodes.Add(transform);
        et.isSpawning.Add(true);
        et.hasSpawned.Add(false);
        et.playerDistance.Add(0.0f);
    }
    //Properly deletes the node by removing it from the list and destroys itself
    public void DeleteNode()
    {
        et.nodes.Remove(transform);
        Destroy(gameObject);
    }
}
