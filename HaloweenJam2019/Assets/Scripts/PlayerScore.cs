using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public Text scoreText;
    public int score;
    public bool isScoring;
    public GameObject player;


    private void Start()
    {
        isScoring = true;
        StartCoroutine("Score");
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        PlayerHealth isAlive = player.GetComponent<PlayerHealth>();
        if (isAlive.isAlive == false)
        {
            isScoring = false;
            Cursor.lockState = CursorLockMode.None;
            player.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<GunUse>().enabled = false;
        }

        if (isScoring == false)
        {
            StopCoroutine("Score");
            scoreText.text = "Time Survived:" + (int)score;
        }
    }

    IEnumerator Score()
    {
        score++;
        yield return new WaitForSeconds(1);
    }
}
