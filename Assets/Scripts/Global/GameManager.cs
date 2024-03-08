using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private float _timeRemaining = 1800f;
        
        private float _gameScore;
        private string _gameTimer;

        public float GameScore => _gameScore;
        public string GameTimer => _gameTimer;

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
                _gameTimer = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }

        public void AddToGameScore(float miniGameScore)
        {
            _gameScore += miniGameScore;
        }
    }
}

