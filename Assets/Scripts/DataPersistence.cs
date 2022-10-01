using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class DataPersistence : MonoBehaviour
{
    public static DataPersistence Instance;

    public TMP_InputField inputField;

    public string currentPlayer;
    public int bestScore;
    public string bestPlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SaveCurrentPlayer()
    {
        currentPlayer = inputField.text;
    }

    [System.Serializable]
    class SaveData
    {
        public int savedScore;
        public string savedPlayer;
    }

    public void SaveHighscore(int playerScore, string playerName)
    {
        SaveData data = new SaveData();
        data.savedScore = playerScore;
        data.savedPlayer = playerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestPlayer = data.savedPlayer;
            bestScore = data.savedScore;
        }

    }
}
