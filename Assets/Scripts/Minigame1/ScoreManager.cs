using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance

    public TextMeshProUGUI scoreText; // TextMeshPro UI element to display the score
    private int score = 0; // Current score

    void Awake()
    {
        // Ensures only one instance of ScoreManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this object when loading new scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    // Function to add score
    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    // Function to update the score text UI
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
