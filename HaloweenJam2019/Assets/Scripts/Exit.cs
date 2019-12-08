using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [Header("Activity Bools")]
    [Tooltip("Wheather or not the player can interact with this terminal")]
    public bool isActive = true;
    [Tooltip("If the player is in range for interaction")]
    public bool inRange;
    [Tooltip("If the terminal has been activated")]
    public bool activated;
    [Header("Locations to Move to")]
    [SerializeField]private GameObject[] MoveLocations;
    [Header("UI Things")]
    [SerializeField] private Text interactText;

    
    //Player refrence. Aquired by the player interacting with the terminal
    private GameObject player;
    [Header("Useful Refernces/Other")]
    [SerializeField]private LevelManager lvlManager;
    [SerializeField] private bool SpawnRoomTerminal;
    public GameObject spawnPoint;

    private void Start()
    {
        interactText = GameObject.FindGameObjectWithTag("InteractText").GetComponent<Text>();
        
        if (!SpawnRoomTerminal)
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex(1));
        }
        lvlManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        if (spawnPoint != null)
        {
            lvlManager.Spawnpoints.Add(spawnPoint);
        }
    }
    private void Update()
    {
        if(isActive && inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Activate();
            }
        }
        else
        {
            interactText.enabled = false;
        }
    }

    private void Activate()
    {
        if (!SpawnRoomTerminal)
        {
            print("SpawnPoint: Moving player to spawn room");
            activated = true;
            print("Sent Player to SpawnRoom");
            lvlManager.MovePlayerToSpawnRoom();
            if (GameObject.FindGameObjectWithTag("EnemyTrickle").GetComponent<EnemyTrickle>().HandleSceneSwitch())
            {
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoadRandomScene();
            }
            
        }
        Debug.Log("I Have Been Pressed");
        if (SpawnRoomTerminal)
        {
            lvlManager.MovePlayerToMap();
            
            isActive = false;
        }

        inRange = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            
            player = other.gameObject;
            inRange = true;
            if (isActive)
            {
                interactText.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            inRange = false;
            interactText.enabled = false;
        }
    }
}
