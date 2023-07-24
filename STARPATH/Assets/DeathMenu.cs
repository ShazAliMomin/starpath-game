using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    [SerializeField] public GameObject deathMenuUI;
    [SerializeField] private Image background;
    private float fadeRate = 0.025f;
    private float fadeDelay = 5.0f;
    public string activeScene;
    
    void Start()
    {
        deathMenuUI = GameObject.Find("DeathMenu");
        deathMenuUI.SetActive(false);
    }

    void Update()
    {
        if (GamePaused == true)
        {
            fadeDelay -= Time.unscaledDeltaTime;
            if(fadeDelay <= 0)
            {
                background.color = new Color(background.color.r, background.color.g, background.color.b, background.color.a + (Time.unscaledDeltaTime * fadeRate));
            }
            
        }
    }

    public void ActivateDeathMenu()
    {
        SoundManager.SoundManagerInstance.ChangeMusicTrack(14);
        Debug.Log("Menu called");
        deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GamePaused = false;
        

        if (SceneManager.GetActiveScene().name == "Level 1")
        {

            hubmanager.sbarr[10] = false;
            hubmanager.sbarr[11] = false;
            hubmanager.sbarr[12] = false;
            hubmanager.sbarr[13] = false;
            hubmanager.sbarr[14] = false;
            hubmanager.sbarr[15] = false;
            hubmanager.sbarr[16] = false;
        }

        if (SceneManager.GetActiveScene().name == "dock level")
                {
            hubmanager.sbarr[17] = false;

        }

        AbilityManager.AbilityManagerInstance.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void LoadMenu()
    {
        GamePaused = false;
        Time.timeScale = 1f;
        SoundManager.SoundManagerInstance.ChangeMusicTrack(6);
      
        AbilityManager.AbilityManagerInstance.Save();
        SceneManager.LoadScene("StartScreen");


    }
    public void LoadTown()
    {
        GamePaused = false;
        Time.timeScale = 1f;
        SoundManager.SoundManagerInstance.ChangeMusicTrack(6);
        activeScene = SceneManager.GetActiveScene().name;
        AbilityManager.AbilityManagerInstance.SetLastScene(activeScene);
      
        SceneManager.LoadScene("Town");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
