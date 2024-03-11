using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    public float pointsPerSecondRemaining = 10f; // Points to add per second remaining
    private float timeRemaining; // Time remaining
    private bool isTimerRunning = false; // Flag to control timer state

    public TextMeshProUGUI timerText; // TextMeshPro text to display the timer

    void Start()
    {
        timeRemaining = totalTime;
        UpdateTimerText();
        StartTimer(); // Start the timer automatically when the scene is played
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            if (timeRemaining <= 0f)
            {
                EndGame();
            }
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    void EndGame()
    {
        isTimerRunning = false;
        CalculatePoints();
    }

    void CalculatePoints()
    {
        float points = timeRemaining * pointsPerSecondRemaining;
        // Add points to the score manager or wherever you keep track of points
        ScoreManager.instance.AddScore((int)points);
    }
}
