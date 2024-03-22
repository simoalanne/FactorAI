using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _gameLengthInSeconds = 1800f;
        [SerializeField] private float _processLengthInSeconds = 180f;
        [SerializeField] private string _activeMiniGameName = "Minigame1";
        [SerializeField] private float _scoreRequiredForAISkip = 50000f;
        

        private float _scoreGatheredForAISkip = 0f;
        private float _gameScore;
        private string _gameTimer;
        private string _processTimer;
        private float _originalProcessLength;
        private bool _isAISkipReady = false;

        public float GameScore => _gameScore;
        public string GameTimer => _gameTimer;
        public string ProcessTimer =>  _processTimer;
        

        public string ActiveMiniGameName
        {
            get { return _activeMiniGameName; }
            set { _activeMiniGameName = value; }
        }

        public bool IsAiSkipReady
        {
            get { return _isAISkipReady; }
            set { _isAISkipReady = value; }
        }

        // Keys for PlayerPrefs
        private readonly string gameLengthInSecondsKey = "GameLengthInSeconds";
        private readonly string processLengthInSecondsKey = "ProcessLengthInSeconds";
        private readonly string gameScoreKey = "GameScore";
        private readonly string isAiSkipReadyKey = "IsAiSkipReady";
        private readonly string aiSkipGatheredScoreKey = "AiSkipGatheredScore";
        private readonly string activeMiniGameNameKey = "ActiveMiniGameName";

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            _originalProcessLength = _processLengthInSeconds;

            _gameScore = PlayerPrefs.GetFloat(gameScoreKey, 0f);
            _gameLengthInSeconds = PlayerPrefs.GetFloat(gameLengthInSecondsKey, 1800f);
            _processLengthInSeconds = PlayerPrefs.GetFloat(processLengthInSecondsKey, 180f);
            _isAISkipReady = PlayerPrefs.GetInt(isAiSkipReadyKey, 0) == 1;
            _scoreGatheredForAISkip = PlayerPrefs.GetFloat(aiSkipGatheredScoreKey, 0f);
            _activeMiniGameName = PlayerPrefs.GetString(activeMiniGameNameKey, "Minigame1");
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name != "Title")
            {
                _gameLengthInSeconds -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(_gameLengthInSeconds / 60);
                int seconds = Mathf.FloorToInt(_gameLengthInSeconds % 60);
                _gameTimer = string.Format("{0:00}:{1:00}", minutes, seconds);

                _processLengthInSeconds -= Time.deltaTime;
                int processMinutes = Mathf.FloorToInt(_processLengthInSeconds / 60);
                int processSeconds = Mathf.FloorToInt(_processLengthInSeconds % 60);
                _processTimer = string.Format("{0:00}:{1:00}", processMinutes, processSeconds);
            }

            if (_processLengthInSeconds <= 0f && SceneManager.GetActiveScene().name == "Factory")
            {
                ChangeActiveMiniGame();
                _processLengthInSeconds = _originalProcessLength;
            }
        }

        public void AddToGameScore(float minigameScore)
        {
            _gameScore += minigameScore;
        }

        public void AddAiSkipProgress(float minigameScore)
        {
            _scoreGatheredForAISkip += minigameScore;
            if (_scoreGatheredForAISkip >= _scoreRequiredForAISkip)
            {
                _isAISkipReady = true;
                _scoreGatheredForAISkip = 0f;
            }
        }

        public void ChangeActiveMiniGame()
        {
            _processLengthInSeconds = _originalProcessLength;

            if (ActiveMiniGameName == "Minigame1")
            {
                ActiveMiniGameName = "Minigame2";
            }
            else
            {
                ActiveMiniGameName = "Minigame1";
            }
        }

        private void OnApplicationPause()
        {
            PlayerPrefs.SetFloat(gameLengthInSecondsKey, _gameLengthInSeconds);
            PlayerPrefs.SetFloat(processLengthInSecondsKey, _processLengthInSeconds);
            PlayerPrefs.SetFloat(gameScoreKey, _gameScore);
            PlayerPrefs.SetInt(isAiSkipReadyKey, _isAISkipReady ? 1 : 0);
            PlayerPrefs.SetFloat(aiSkipGatheredScoreKey, _scoreGatheredForAISkip);
            PlayerPrefs.SetString(activeMiniGameNameKey, _activeMiniGameName);
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetFloat(gameLengthInSecondsKey, _gameLengthInSeconds);
            PlayerPrefs.SetFloat(processLengthInSecondsKey, _processLengthInSeconds);
            PlayerPrefs.SetFloat(gameScoreKey, _gameScore);
            PlayerPrefs.SetInt(isAiSkipReadyKey, _isAISkipReady ? 1 : 0);
            PlayerPrefs.SetFloat(aiSkipGatheredScoreKey, _scoreGatheredForAISkip);
            PlayerPrefs.SetString(activeMiniGameNameKey, _activeMiniGameName);
        }
    }
}
