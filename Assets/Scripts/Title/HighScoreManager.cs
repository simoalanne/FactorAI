using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HighScoreManager : MonoBehaviour
{
    private const string HighScoreFileName = "/highscores.dat";
    private List<float> _highScores;

    public List<float> HighScores => _highScores;

    private void Awake()
    {
        _highScores = LoadHighScores();
    }

    public void SaveHighScores(List<float> highScores)
    {
        string path = Application.persistentDataPath + HighScoreFileName;
        using Stream stream = new FileStream(path, FileMode.OpenOrCreate);
        new BinaryFormatter().Serialize(stream, highScores);
    }

    public List<float> LoadHighScores()
    {
        string path = Application.persistentDataPath + HighScoreFileName;

        if (!File.Exists(path))
        {
            return new List<float>();
        }

        using Stream stream = new FileStream(path, FileMode.Open);
        return (List<float>)new BinaryFormatter().Deserialize(stream);
    }
}
