using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeathUI : MonoBehaviour
{
    public Canvas canvas;
    public GameObject eventSystem;

    void Start()
    {
        canvas.enabled = false;
        //eventSystem = GameObject.FindGameObjectWithTag("EventSystem");
    }

    private void Update()
    {
        PlayerScore isScoring = GetComponent<PlayerScore>();
        if (isScoring.isScoring == false)
        {
            canvas.enabled = true;
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }

}
