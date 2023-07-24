using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    //public GameData data;
    string dataPath;

    private Queue<string> sentences;

    private void Start() 
    {
        sentences = new Queue<string>();
        dataPath = Application.persistentDataPath + "/gameData.json";
    }
    
    
    
    public void SaveGameData( int currentLevel, float health, int dash)
    {

        GameData data = new GameData();
        //data.inventory = inventory;
        data.currentLevel = currentLevel;
        data.health = health;
        data.dashLevel = dash;

        string jsonData = JsonUtility.ToJson(data);
        Debug.Log(dataPath);
        File.WriteAllText(dataPath, jsonData);
        Debug.Log("Save SuccessFull");
    }

    public GameData LoadGameData()
    {
        if (File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);
            return data;
        }
        else
        {
            Debug.LogError("Game data file not found!");
            return null;
        }
    }
}

[System.Serializable]
public class GameData
{
    //var InventoryItem inventory;
    public int currentLevel;
    public float health;
    public int dashLevel;
}

//[System.Serializable]
//public class InventoryItem
//{
//    public string name;
//}