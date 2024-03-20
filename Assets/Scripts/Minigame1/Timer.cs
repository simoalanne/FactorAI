using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Global;

public class Timer : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    public float pointsPerSecondRemaining = 10f; // Points to add per second remaining
    private float timeRemaining; // Time remaining
    private bool isTimerRunning = false; // Flag to control timer state
    private ScoreManager _scoreManager;

    public TextMeshProUGUI timerText; // TextMeshPro text to display the timer

    void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
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
        MoveToMenuScene();
    }

    void MoveToMenuScene()
    {
        GameManager.Instance.AddToGameScore(_scoreManager.Score);
        SceneManager.LoadSceneAsync("Factory");
    }
}