using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI, optionsMenuUI, controlsMenuUI;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            } else 
            {
                PauseGame();
            }
        }
    }
    
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SoundManager.SoundManagerInstance.ChangeMusicTrack(6);
        SceneManager.LoadScene("StartScreen");
    }

    public void QuitGame() 
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
