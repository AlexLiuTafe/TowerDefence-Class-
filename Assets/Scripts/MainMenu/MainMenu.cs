using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [Header("ONLY IF GAME SCENE")]
    public bool gameScene;
    public bool paused;
    public GameObject pausePanel;
    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        if(gameScene)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (!paused)
                {
                    Pause();
                }
                else
                {
                    Resume();                                                                                                                       
                }
            }
        }
    }
    public void PlayGame(int i)
    {
        SceneManager.LoadScene(i);

    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QuiT");

    }

    void Pause()
    {
        paused = true; 
        Time.timeScale = 0; // stop the time
        pausePanel.SetActive(true); // make pause panel appear
    }

    public void Resume()
    {
        Time.timeScale = 1; // Make the time start
        paused = false; 
        pausePanel.SetActive(false); //make pause panel disappear
    }

}
