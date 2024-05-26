using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[Serializable]
public class HighScore
{
    public string PlayerName;
    public float Score;
    public DateTime Date;

    public HighScore(string playerName, float score)
    {
        PlayerName = playerName;
        Score = score;
        Date = DateTime.Now;
    }
}

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    private const string HighScoreFileName = "/highscores.dat";
    private List<HighScore> _highScores;
    private bool _newHighScore = false;

    public List<HighScore> HighScores => _highScores;
    public bool NewHighScore => _newHighScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _highScores = LoadHighScores();
        _highScores.Sort((a, b) => b.Score.CompareTo(a.Score)); // Sort in descending order
    }

    public void SaveHighScores(List<HighScore> highScores)
    {
        string path = Application.persistentDataPath + HighScoreFileName;
        using Stream stream = new FileStream(path, FileMode.OpenOrCreate);
        new BinaryFormatter().Serialize(stream, highScores);
    }

    private List<HighScore> LoadHighScores()
    {
        string path = Application.persistentDataPath + HighScoreFileName;

        if (!File.Exists(path))
        {
            return new List<HighScore>();
        }

        try
        {
            using Stream stream = new FileStream(path, FileMode.Open);
            return (List<HighScore>)new BinaryFormatter().Deserialize(stream);
        }
        catch (InvalidCastException)
        {
            Debug.LogWarning("Failed to load high scores due to incompatible saved data. Returning a new high score list.");
            return new List<HighScore>();
        }
    }
    public void CheckForHighScore(float score)
    {
        _newHighScore = false;

        // if there are less than 3 high scores add score instantly
        if (_highScores.Count < 3)
        {
            _newHighScore = true;
        }

        // if score is higher than the lowest high score, set _newHighScore to true
        else if (_highScores[2].Score < score)
        {
            _newHighScore = true;
        }
    }

    public void AddHighScore(string playerName, float score)
    {
        _highScores.Add(new HighScore(playerName, score));
        _highScores.Sort((a, b) => b.Score.CompareTo(a.Score)); // Sort in descending order
        SaveHighScores(_highScores);
        _highScores = LoadHighScores();
    }

    public void ResetHighScores()
    {
        _highScores.Clear();
        SaveHighScores(_highScores);
        _highScores = LoadHighScores();
    }
}