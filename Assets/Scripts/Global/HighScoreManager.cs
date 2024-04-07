using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    private const string HighScoreFileName = "/highscores.dat";
    private List<float> _highScores;
    private bool _newHighScore = false;

    public List<float> HighScores => _highScores;
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
        _highScores.Sort();
        _highScores.Reverse();
    }

    public void SaveHighScores(List<float> highScores)
    {
        string path = Application.persistentDataPath + HighScoreFileName;
        using Stream stream = new FileStream(path, FileMode.OpenOrCreate);
        new BinaryFormatter().Serialize(stream, highScores);
    }

    private List<float> LoadHighScores()
    {
        string path = Application.persistentDataPath + HighScoreFileName;

        if (!File.Exists(path))
        {
            return new List<float>();
        }

        using Stream stream = new FileStream(path, FileMode.Open);
        return (List<float>)new BinaryFormatter().Deserialize(stream);
    }

    public void CheckForHighScore(float score)
    {
        _newHighScore = false;

        // if there are less than 3 high scores add score instantly
        if (_highScores.Count < 3)
        {
            AddHighScore(score);
        }

        // if score is higher than the lowest high score, remove the lowest and add the new score
        else if (HighScores[2] < score)
        {
            _highScores.RemoveAt(2);
            AddHighScore(score);
        }
    }

    private void AddHighScore(float score)
    {
        _newHighScore = true;
        _highScores.Add(score);
        _highScores.Sort();
        _highScores.Reverse();
        SaveHighScores(_highScores);
        _highScores = LoadHighScores();
    }
}



