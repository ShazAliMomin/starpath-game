using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager AbilityManagerInstance;

    [SerializeField] public int pistolLevel, smgLevel, shotgunLevel;
    [SerializeField] public int aoeLevel, dashLevel, turretLevel, orbsLevel;

    public int upgradePointsAvailable;
    public int playerLevel;

    public int playerShootMode;
    public bool playerPistolAltFire, playerSMGAltFire, playerShotgunAltFire;

    public float playerHealth;
    public int playerMaxHealth;
    public float playerXP;
    public float playerMaxXP;
    public bool controllerDetected;

    string filePath;


    public string sceneName;

    bool[] upgradeUIButtons;
    bool[] upgradeUIButtonsAvailable;

    public int rofLevelPistol, damageLevelPistol, damageLevelSMG, capacityLevelSMG, projectileLevelShotgun, spreadLevelShotgun, dashCooldownLevel, projectileLevelTurret, rangeLevelAOE, cooldownLevelAOE, meleeDamageLevel;
    public bool threeBurstUnlocked, stunGunUnlocked, ignoreResistanceUnlockedSMG, overchargeUnlocked, grenadeLauncherUnlocked, slamfireUnlocked, shoveUnlocked, invincUnlocked, ricochetUnlocked, stunExplodeUnlocked, stunAOEUnlocked, meleeResistanceUnlocked;
    public bool smgUnlocked, shotgunUnlocked, turretUnlocked;

    public bool[] questLevelUps;
    public static bool zeke = false;
    public static bool freya = false;
    public static bool norman = false;
    public static bool janna = false;
   
    public bool isContinue = false;
    // Freya = 0, Norman = 1, Zeke = 2, Janna = 3, Shield Door = 4
    string saveVariablesPath = "../upgradeUIButtons.dat";
    public void unlockWeapons()
    {
        if (smgUnlocked == true) return;
        upgradeUIButtons[0] = true;
        upgradeUIButtons[13] = true;
        upgradeUIButtons[22] = true;
        smgUnlocked = true;
        shotgunUnlocked = true;
    }

    public void unlockTurret()
    {
        if (turretUnlocked == true) return;
        upgradeUIButtons[1] = true;
        upgradeUIButtons[41] = true;
        turretUnlocked = true;
    }

    private void initializeAbilities()
    {
        //new game only
        meleeDamageLevel = 1;
        rofLevelPistol = 1;
        damageLevelPistol = 1;
        damageLevelSMG = 1;
        capacityLevelSMG = 1;
        projectileLevelShotgun = 1;
        spreadLevelShotgun = 1;
        dashCooldownLevel = 1;
        projectileLevelTurret = 1;
        rangeLevelAOE = 1;
        cooldownLevelAOE = 1;

        meleeResistanceUnlocked = false;
        threeBurstUnlocked = false;
        stunGunUnlocked = false;
        ignoreResistanceUnlockedSMG = false;
        overchargeUnlocked = false;
        grenadeLauncherUnlocked = false;
        slamfireUnlocked = false;
        shoveUnlocked = false;
        invincUnlocked = false;
        ricochetUnlocked = false;
        stunExplodeUnlocked = false;
        stunAOEUnlocked = false;

        smgUnlocked = false;
        shotgunUnlocked = false;
        turretUnlocked = false;

        upgradeUIButtons[2] = true;
        upgradeUIButtons[31] = true;
        upgradeUIButtons[35] = true;
        upgradeUIButtons[47] = true;
    }

    public void UnlockUpgradeFromMenu(int i)
    {
        if (i == 3 || i == 4 || i == 6 || i == 7) rofLevelPistol++;
        if (i == 8 || i == 9 || i == 11 || i == 12) damageLevelPistol++;
        if (i >= 14 && i <= 16) damageLevelSMG++;
        if (i >= 18 && i <= 20) capacityLevelSMG++;
        if (i == 23 || i == 24 || i == 29 || i == 30) projectileLevelShotgun++;
        if (i == 26 || i == 27) spreadLevelShotgun++;
        if (i == 32 || i == 33) meleeDamageLevel++;
        if (i >= 37 && i <= 40) dashCooldownLevel++;
        if (i == 42 || (i >= 44 && i <= 46)) projectileLevelTurret++;
        if (i == 48 || (i >= 50 && i <= 52)) cooldownLevelAOE++;
        if (i == 53 || i == 54) rangeLevelAOE++;


        if (i == 5) threeBurstUnlocked = true;
        if (i == 10) stunGunUnlocked = true;
        if (i == 17) ignoreResistanceUnlockedSMG = true;
        if (i == 21) overchargeUnlocked = true;
        if (i == 25) grenadeLauncherUnlocked = true;
        if (i == 28) slamfireUnlocked = true;
        if (i == 34) meleeResistanceUnlocked = true;
        if (i == 36) shoveUnlocked = true; 
        if (i == 43) ricochetUnlocked = true;
        if (i == 49) stunAOEUnlocked = true;
    }


    void Awake()
    {
        filePath = "../gameSaveData4.json";
        
        if (AbilityManagerInstance == null)
        {
            AbilityManagerInstance = this;
            DontDestroyOnLoad(gameObject);

            //check if previous save exists and initialize values to that, when saves are implemented
		
			upgradeUIButtons = new bool[60];
			//upgradeUIButtonsAvailable = new bool[60];

			questLevelUps = new bool[5];

			initializeAbilities();

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                GameDatas data = JsonUtility.FromJson<GameDatas>(jsonData);

                if (File.Exists(saveVariablesPath))
                {
                    /*string json = File.ReadAllText(filePath);

                    bool[] loadedArray = JsonUtility.FromJson<UpgradeUIButtonsData>(json);*/
                   
                    bool[] loadedArray = LoadArrayFromFile(saveVariablesPath);
                    for (int i = 0; i < loadedArray.Length; i++)
                    {
                        if (i < loadedArray.Length)
                        {
                            upgradeUIButtons[i] = loadedArray[i];
                        }
                     
                    }
                }

                // s.re the upgrade points available and player level

                pistolLevel = data.pistolLevel;
                smgLevel = data.smgLevel;
                shotgunLevel = data.shotgunLevel;

                aoeLevel = data.aoeLevel;
                dashLevel = data.dashLevel;
                turretLevel = data.turretLeve;
                orbsLevel = data.orbsLevel;
                //sceneName = data.sceneName;

                zeke = data.zeke;
                norman = data.norman;
                freya = data.freya;
                playerShootMode = data.playerShootMode;

            //Implement loading system when not starting new game
            


                playerPistolAltFire = data.playerPistolAltFire;
                playerSMGAltFire = data.playerSMGAltFire;
                playerShotgunAltFire = data.playerShotgunAltFire;

                playerHealth = data.playerHealth;
                playerMaxHealth = data.playerMaxHealth;
                playerXP = data.playerXP;
                playerMaxXP = data.playerMaxXP;

                upgradePointsAvailable = data.upgradePointsAvailable;
             

                playerLevel = data.playerLevel;
            }
            else
            {
                pistolLevel = smgLevel = shotgunLevel = 1;

                aoeLevel = dashLevel = turretLevel = orbsLevel = 1;
               
                zeke = false;
                norman = false;
                freya = false;

                //Implement loading system when not starting new game

                playerShootMode = 1;
                playerPistolAltFire = playerSMGAltFire = playerShotgunAltFire = false;

                playerHealth = 15;
                playerMaxHealth = 15;
                playerXP = 0;
                playerMaxXP = 100f;

                upgradePointsAvailable = 0;
                playerLevel = 1;
              
                //sceneName = SceneManager.LoadScene(SceneManager.GetActiveScene().name).;
            }
        }
    }

    public void setBaseUpgrades()
    {
        upgradeUIButtons[2] = true;
        upgradeUIButtons[31] = true;
        upgradeUIButtons[35] = true;
        upgradeUIButtons[47] = true;
    }

    public bool questLevelUp(int index)
    {
        if (questLevelUps[index] == true) return false;
        else
        {
            questLevelUps[index] = true;
            return true;
        }
    }

    public void SetAbility(int index, int value)
    {
        switch (index)
        {
            case 1:
                pistolLevel = value;
                break;
            case 2:
                smgLevel = value;
                break;
            case 3:
                shotgunLevel = value;
                break;
            case 4:
                aoeLevel = value;
                break;
            case 5:
                dashLevel = value;
                break;
            case 6:
                turretLevel = value;
                break;
            case 7:
                orbsLevel = value;
                break;
            default:
                Debug.Log("Out of bounds.");
                break;
        }

        return;
    }

    public int GetAbility(int index)
    {
        switch (index)
        {
            case 1:
                return pistolLevel;
            case 2:
                return smgLevel;
            case 3:
                return shotgunLevel;
            case 4:
                return aoeLevel;
            case 5:
                return dashLevel;
            case 6:
                return turretLevel;
            case 7:
                return orbsLevel;
            default:
                Debug.Log("Out of bounds.");
                return -1;
        }
        
    } 

    public void updateUpgradeUI(int index, bool status)
    {
        upgradeUIButtons[index] = status;
    }

    public bool getUpgradeUIStatus(int index)
    {
        return upgradeUIButtons[index];
    }

    public int getUnlockLevel(int indexA, int indexB)
    {
        if (indexA == 1) { if (indexB == 0) return rofLevelPistol; else return damageLevelPistol; }
        if (indexA == 2) { if (indexB == 0) return damageLevelSMG; else return capacityLevelSMG; }
        if(indexA == 3) { if (indexB == 0) return projectileLevelShotgun; else return spreadLevelShotgun; }
        if (indexA == 4) { if (indexB == 0) return rangeLevelAOE; else return cooldownLevelAOE; }
        if (indexA == 5) return dashCooldownLevel;
        if (indexA == 6) return projectileLevelTurret;
        if (indexA == 7) return meleeDamageLevel;

        Debug.Log("invalid indexes");
        return -1;
    }

    public bool getUnlockAbility(int indexA, int indexB)
    {
        if(indexA == 1) { if (indexB == 0) return threeBurstUnlocked; else return stunGunUnlocked; }
        if(indexA == 2) { if (indexB == 0) return ignoreResistanceUnlockedSMG; else return overchargeUnlocked; }
        if (indexA == 3) { if (indexB == 0) return grenadeLauncherUnlocked; else return slamfireUnlocked; }
        if (indexA == 4) return stunAOEUnlocked;
        if(indexA == 5) { if (indexB == 0) return shoveUnlocked; else return invincUnlocked; }
        if(indexA == 6) { if (indexB == 0) return ricochetUnlocked; else return stunExplodeUnlocked; }
        if (indexA == 7) return meleeResistanceUnlocked;

        Debug.Log("invalid indexes");
        return false;
    }
    public void SetController(bool status)
    {
        controllerDetected = status;
    }

    public bool GetController()
    {
        return controllerDetected;
    }
    public float GetHealth()
    {
        return playerHealth;
    }
    public void SetHealth(float h)
    {
        playerHealth = h;
    }
    public string GetLastScene()
    {
        return sceneName;
    }
    public void SetLastScene(string name)
    {
        sceneName = name.ToString();
    }

    public float GetXP()
    {
        return playerXP;
    }
    public void SetXP(float h)
    {
        playerXP = h;
    }

    public int GetPlayerLevel()
    {
        return playerLevel;
    }

    public float GetMaxXP()
    {
        return playerMaxXP;
    }
    public void SetMaxXP(float x)
    {
        playerMaxXP = x;
    }

    public int GetMaxHealth()
    {
        return playerMaxHealth;
    }

    public void SetMaxHealth(int h)
    {
        playerMaxHealth = h;
    }

    public int GetShootMode()
    {
        return playerShootMode;
    }
    public void SetShootMode(int h)
    {
        playerShootMode = h;
    }
    public void SetPlayerLevel(int l)
    {
        playerLevel = l;
    }

    public bool GetAltFire(int i)
    {
        switch (i)
        {
            case 1:
                return playerPistolAltFire;
            case 2:
                return playerSMGAltFire;
            case 3:
                return playerShotgunAltFire;
            default:
                return false;
        }
    }

    public void SetAltFire(int i, bool f)
    {
        switch (i)
        {
            case 1:
                playerPistolAltFire = f;
                break;
            case 2:
                playerSMGAltFire = f;
                break;
            case 3:
                playerShotgunAltFire = f;
                break;
            default:
                return;
        }
    }

    public void setFreyaStatus(bool fr)
    {
        freya = fr;
        
    }

    public void setNormanStatus(  bool nor)
    {
       
        norman = nor;
    }
    public void setGameContinuationStatus(bool sts)
    {
        isContinue = sts;
    }
    public void setZekeStatus(bool zk)
    {
        
        zeke = zk;
       
    }
    public bool getFreyaStatus()
    { 
        return freya;
    }
    public bool getNormanStatus()
    {
        return zeke;
    }
    public bool getZekeStatus()
    {
        return norman;
    }
    public bool getGameContinuationStatus()
    {
        return isContinue;
    }


    public int GetUpgradePointsAvailable()
    {
        return upgradePointsAvailable;
    }

    public void GainLevel()
    {
        upgradePointsAvailable++;
        playerLevel++;
    }

    public void SpendUpgradePoints(int i)
    {
        upgradePointsAvailable -= i;
    }



   /*
    public void Save(string filePath)
    {
        // create a new dictionary to store the data
        Dictionary<string, object> data = new Dictionary<string, object>();
        Debug.Log("Saveing ....");
        // store the ability levels
        data["pistolLevel"] = pistolLevel;
        data["smgLevel"] = smgLevel;
        data["shotgunLevel"] = shotgunLevel;
        data["aoeLevel"] = aoeLevel;
        data["dashLevel"] = dashLevel;
        data["turretLevel"] = turretLevel;
        data["orbsLevel"] = orbsLevel;

        // store the upgrade points available and player level
        data["upgradePointsAvailable"] = upgradePointsAvailable;
        data["playerLevel"] = playerLevel;

        // store the player shoot mode, alt fire options, health, and experience
        data["playerShootMode"] = playerShootMode;
        data["playerPistolAltFire"] = playerPistolAltFire;
        data["playerSMGAltFire"] = playerSMGAltFire;
        data["playerShotgunAltFire"] = playerShotgunAltFire;
        data["playerHealth"] = playerHealth;
        data["playerMaxHealth"] = playerMaxHealth;
        data["playerXP"] = playerXP;
        data["playerMaxXP"] = playerMaxXP;

        // serialize the data to JSON
        string json = JsonUtility.ToJson(data, true);
       
        // write the JSON to the file
        File.WriteAllText(filePath, json);
    }*/


    public void Save()
    {
        filePath = "../gameSaveData4.json";
        GameDatas data = new GameDatas();
        // store the ability levels
        data.pistolLevel = pistolLevel;
        data.smgLevel = smgLevel;
        data.shotgunLevel = shotgunLevel;
        data.aoeLevel = aoeLevel;
        data.dashLevel = dashLevel;
        data.turretLeve = turretLevel;
        data.orbsLevel = orbsLevel;

        // s.re the upgrade points available and player level
        data.upgradePointsAvailable = upgradePointsAvailable;
        data.playerLevel = playerLevel;

        //s.re the player shoot mode, alt fire options, health, and experience
        data.playerShootMode = playerShootMode;
        data.playerPistolAltFire = playerPistolAltFire;
        data.playerSMGAltFire = playerSMGAltFire;
        data.playerShotgunAltFire = playerShotgunAltFire;
        data.playerHealth = playerHealth;
        data.playerMaxHealth = playerMaxHealth;
        data.playerXP = playerXP;
        data.playerMaxXP = playerMaxXP;
        data.zeke = zeke;
        data.norman = norman;
        data.freya = freya;
        //data.sceneName = sceneName;
   
        // serialize the data to JSON
        string json = JsonUtility.ToJson(data, true);

        //Debug.Log("Save Successfull");
        // write the JSON to the file
        File.WriteAllText(filePath, json);

      
        if (upgradeUIButtons != null)
        {

            // save the JSON to file
            // hasib
            /* string jsonState = JsonUtility.ToJson(upgradeUIButtons); ;
             System.IO.File.WriteAllText(saveVariablesPath, jsonState);*/
           
            SaveArrayToFile(saveVariablesPath, upgradeUIButtons);
        }

    }

    public void Reset()
    {
        string filePath = "../gameSaveData4.json";

        //create a file sample.txt in current working directory 
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        pistolLevel = smgLevel = shotgunLevel = 1;

        aoeLevel = dashLevel = turretLevel = orbsLevel = 1;

        freya = zeke = norman = false;

        //Implement loading system when not starting new game

        playerShootMode = 1;
        playerPistolAltFire = playerSMGAltFire = playerShotgunAltFire = false;

        playerHealth = 15;
        playerMaxHealth = 15;
        playerXP = 0;
        playerMaxXP = 100f;

        upgradePointsAvailable = 0;
        playerLevel = 1;
        // Delete the file
        string filePath2 = "../globalDialogueVariables.json";
        upgradeUIButtons = new bool[60];
        //create a file sample.txt in current working directory 
        if (File.Exists(filePath2))
        {
            File.Delete(filePath2);
        }
        if (File.Exists(saveVariablesPath))
        {
            File.Delete(filePath2);
        }
        PlayerPrefs.DeleteAll();
    }
    public void SaveArrayToFile(string fPath, bool[] array)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(fPath, FileMode.Create)))
        {
            // Write the length of the array to the file
            writer.Write(array.Length);

            // Write the boolean values of the array to the file
            foreach (bool value in array)
            {
                writer.Write(value);
            }
        }
    }

    // Load the array from a binary file
    public bool[] LoadArrayFromFile(string fPath)
    {
        bool[] array = null;

        using (BinaryReader reader = new BinaryReader(File.Open(fPath, FileMode.Open)))
        {
            // Read the length of the array from the file
            int length = reader.ReadInt32();

            // Create a new array with the same length as the one saved in the file
            array = new bool[length];

            // Read the boolean values of the array from the file
            for (int i = 0; i < length; i++)
            {
                array[i] = reader.ReadBoolean();
            }
        }

        return array;
    }

}


[System.Serializable]
public class GameDatas
{
    public int pistolLevel;
    public int smgLevel;
    public int shotgunLevel;
    public int dashLevel;
    public int playerLevel;
    public int playerShootMode;
    public bool playerPistolAltFire;
    public bool playerSMGAltFire;
    public bool playerShotgunAltFire;
    public float playerHealth;
    public int playerMaxHealth;
    public float playerXP;
    public float playerMaxXP;
    public int aoeLevel;
    public int turretLeve ;
    public int orbsLevel ;
    public int upgradePointsAvailable;
    public bool freya;
    public bool norman;
    public bool zeke;
   
   // public string sceneName;
}
// Save the array to a binary file

//public GameDatas LoadGameData(dataPath)
//{
//    if (File.Exists(dataPath))
//    {
//        string jsonData = File.ReadAllText(dataPath);
//        SaveDatas data = JsonUtility.FromJson<SaveDatas>(jsonData);
//        return data;
//    }
//    else
//    {
//        Debug.LogError("Game data file not found!");
//        return null;
//    }
//}