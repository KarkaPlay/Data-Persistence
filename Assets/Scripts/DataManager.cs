using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public InputField inputField;
    public Text bestScoreText;

    public string currentPlayerName;
    public int currentPlayerHighestScore = 0;
    
    [SerializeField] private string _path;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _path = Application.persistentDataPath + "/save.json";
        
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerData(PlayerData playerData)
    {
        currentPlayerName = playerData.playerName;
        currentPlayerHighestScore = playerData.highestScore;
        bestScoreText.text = $"{currentPlayerName} : Best Score: {currentPlayerHighestScore}";
    }

    public void SetPath()
    {
        currentPlayerName = inputField.text;
        _path = Application.persistentDataPath + "/" + currentPlayerName + ".json";
    }

    [Serializable]
    public class PlayerData
    {
        public string playerName;
        public int highestScore;
    }

    public void NewPlayer()
    {
        SetPath();
        PlayerData playerData = new PlayerData { highestScore = 0, playerName = currentPlayerName};

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(_path, json);
        LoadData();
    }

    public void SaveData()
    {
        PlayerData playerData = new PlayerData { highestScore = currentPlayerHighestScore, playerName = currentPlayerName};

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(_path, json);
    }

    public void LoadData()
    {
        SetPath();
        if (File.Exists(_path))
        {
            string json = File.ReadAllText(_path);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            SetPlayerData(playerData);
        }
    }
}
