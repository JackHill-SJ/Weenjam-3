using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject SpawnRoomEntrancePoint;
    public List<GameObject> Spawnpoints;
    public GameObject player;
    public Exit SpawnRoomTerminal;

    private bool FullyLoaded;

    //This is to track whether or not you have to unload a scene when loading a new one
    public int ActiveSceneNum = -1;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //LoadRandomScene();
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
        if(ActiveSceneNum != -1)
        {
            UnloadScene(sceneIndex);
        }
        ActiveSceneNum = sceneIndex;
    }
    public void UnloadScene(int sceneIndex)
    {
        foreach(GameObject point in Spawnpoints)
        {
            Destroy(point.transform.parent.gameObject);
        }
        Spawnpoints.Clear();
        StartCoroutine(UnloadSceneAsync(sceneIndex));
    }  
    public void LoadRandomScene()
    {
        //4 & 5 are the only options as of december 2019
        if(ActiveSceneNum != -1)
        {
            UnloadScene(ActiveSceneNum);
        }
        LoadScene(Random.Range(0, 100) < 50? 4 : 5);
        //LoadScene(5);
    }

    public void MovePlayerToSpawnRoom()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = SpawnRoomEntrancePoint.transform.position;
        player.GetComponent<CharacterController>().enabled = true;
        SpawnRoomTerminal.isActive = true;
    }
    public void MovePlayerToMap()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = Spawnpoints[Mathf.CeilToInt(Random.Range(0, Spawnpoints.Count))].transform.position;
        player.GetComponent<CharacterController>().enabled = true;
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        while (!operation.isDone)
        {
            yield return null;
        }
        //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
        MovePlayerToMap();
        //StartCoroutine(DelayedMapSpawn());
    }

    IEnumerator UnloadSceneAsync(int sceneIndex)
    {
        print("SceneIndex to be unloaded: " + sceneIndex);
        AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator DelayedMapSpawn()
    {
        yield return new WaitForSeconds(1);
        print("Delayed");
        MovePlayerToMap();
    }
}
