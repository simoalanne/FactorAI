using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance

    public TextMeshProUGUI scoreText; // TextMeshPro UI element to display the score
    private float score = 0; // Current score

    public float Score => score;

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
