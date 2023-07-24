using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public static bool seenopening;
    private bool controllerDetected;

    public GameObject mainFirstButton, optionsFirstButton, optionsCloseButton, controlsFirstButton, controlsCloseButton, resetFirstButton, resetCloseButton;

    void Start()
    {
        
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainFirstButton);
    }

    void Update()
    {
        if(Input.GetJoystickNames().Length > 0)
        {
            //Debug.Log("Controller detected: " + Input.GetJoystickNames());
            controllerDetected = true;
            AbilityManager.AbilityManagerInstance.SetController(true);
        }
        else
        {
            controllerDetected = false;
            AbilityManager.AbilityManagerInstance.SetController(false);
        }


    }

    public void OpenOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsCloseButton);
    }

    public void OpenControls()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsFirstButton);
    }

    public void CloseControls()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsCloseButton);
    }

    public void OpenReset()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resetFirstButton);
    }

    public void CloseReset()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resetCloseButton);
    }

    public void PlayGame()
    {

        string filePath = "../gameSaveData4.json";
        AbilityManager.AbilityManagerInstance.Reset();
        //create a file sample.txt in current working directory 
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        string filePath2 = "../globalDialogueVariables.json";

        //create a file sample.txt in current working directory 
        if (File.Exists(filePath2))
        {
            File.Delete(filePath2);
        }
        string saveVariablesPath = "../upgradeUIButtons.dat";
        if (File.Exists(saveVariablesPath))
        {
            File.Delete(saveVariablesPath);
        }
        PlayerPrefs.DeleteAll();
        //AbilityManager.AbilityManagerInstance.setGameContinuationStatus(false);
        //AbilityManager.AbilityManagerInstance.Reset();
       
       
            if (((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("start1")).value == false)
            {
                SceneManager.LoadScene("openreal");
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Debug.Log("Next Scene Loaded");
            }
        

    }
    public void ContinueGame()
    {
        //AbilityManager.AbilityManagerInstance.setGameContinuationStatus(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //AbilityManager.AbilityManagerInstance.setGameContinuationStatus(true);
      
        Debug.Log("continue Scene Loaded");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();

    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("credits");
        Debug.Log("Credits Loaded");
    }

    public void ResetProgress()
    {
        //PlayerPrefs.DeleteAll();
        AbilityManager.AbilityManagerInstance.Reset();
        
        //SceneManager.LoadScene("ResetStoryConfirmation");

        Debug.Log("Player-prefs reset");
    }
    public void SaveGameState()
    {
        AbilityManager.AbilityManagerInstance.Save();
    }

}
