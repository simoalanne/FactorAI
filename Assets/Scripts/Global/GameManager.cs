using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private string _currentMiniGameName = "Minigame2";
        [SerializeField] private float _gameLengthInSeconds = 1800f;
        [SerializeField] private float _scoreRequiredForAISkip = 50000f;

        private float _scoreGatheredForAISkip = 0f;
        private float _gameScore;
        private string _gameTimer;
       
        private bool _isAISkipReady = false;


        public float GameScore => _gameScore;
        public string GameTimer => _gameTimer;

        public string MiniGameName
        {
            get { return _currentMiniGameName; }
            set { _currentMiniGameName = value; }
        }

        public bool IsAiSkipReady
        {
            get { return _isAISkipReady; }
            set { _isAISkipReady = value; }
        }

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
                _gameLengthInSeconds -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(_gameLengthInSeconds / 60);
                int seconds = Mathf.FloorToInt(_gameLengthInSeconds % 60);
                _gameTimer = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }

        public void AddToGameScore(float minigameScore)
        {
            _gameScore += minigameScore;
        }

        public void AiSkipProgress(float minigameScore)
        {
            _scoreGatheredForAISkip += minigameScore;
            if (_scoreGatheredForAISkip >= _scoreRequiredForAISkip)
            {
                _isAISkipReady = true;
                _scoreGatheredForAISkip = 0f;
            }
        }
    }
}
