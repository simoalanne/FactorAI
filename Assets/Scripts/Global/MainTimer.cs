using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTimer : MonoBehaviour
{
    public static MainTimer Instance;
    
    private float _timeRemaining = 1800f;

    private string _printTime;

    public string PrintTime => _printTime;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Title")
        {
            _timeRemaining -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(_timeRemaining / 60);
            int seconds = Mathf.FloorToInt(_timeRemaining % 60);
            _printTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}

