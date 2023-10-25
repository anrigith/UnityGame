using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public int P1Life;
    public int P2Life;

    public GameObject p1Wins;
    public GameObject p2Wins;

    public GameObject[] p1Healfs;
    public GameObject[] p2Healfs;

    private bool isGamePaused = false;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlaySFX(audioManager.starting);
    }

    // Update is called once per frame
    private void Update()
    {
        if (P1Life <= 0)
        {
            player1.SetActive(false);
            p2Wins.SetActive(true);
        }

        if (P2Life <= 0)
        {
            player2.SetActive(false);
            p1Wins.SetActive(true);
        }
        if (isGamePaused && Input.GetKeyDown(KeyCode.Space))
        {
            ReloadScene();
        }
    }
    private void FixedUpdate()
    {
        if (P1Life <= 0 || P2Life <= 0)
        {
            PauseGame();
            isGamePaused = true;
        }

    }

    public void HurtP1()
    {
        P1Life -= 1;

        for (int i = 0; i < p1Healfs.Length; i++)
        {
            if (P1Life > i)
            {
                p1Healfs[i].SetActive(true);
            }
            else
            {
                p1Healfs[i].SetActive(false);
            }
        }
    }

    public void HurtP2()
    {
        P2Life -= 1;

        for (int i = 0; i < p2Healfs.Length; i++)
        {
            if (P2Life > i)
            {
                p2Healfs[i].SetActive(true);
            }
            else
            {
                p2Healfs[i].SetActive(false);
            }
        }
    }

    public void GameOver(string playerTag)
    {
        if (playerTag == "firstPlayer")
        {
            p2Wins.SetActive(true);
        }
        else if (playerTag == "secondPlayer")
        {
            p1Wins.SetActive(true);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}

