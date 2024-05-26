using UnityEngine;
using UnityEngine.SceneManagement;
using Factory;

namespace Global
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _gameLengthInSeconds = 600f;
        [SerializeField] private float _processLengthInSeconds = 180f;
        [SerializeField] private string _currentGameState = "ProductSelection";
        [SerializeField] private float _scoreRequiredForAISkip = 81000f;
        [SerializeField] private float _scorefromWaitingOutMinigame = 10000f;
        [SerializeField] private float _scoreGatheredForAISkip = 0;
        [SerializeField] private float _gameScore;
        [SerializeField] private bool _isAISkipReady = false;
        private string _gameTimer;
        private string _processTimer;
        private float _originalProcessLength;
        private string _currentProduct;
        [SerializeField] private bool _firstTimePlaying = true;

        public float GameLengthInSeconds => _gameLengthInSeconds;
        public float GameScore => _gameScore;
        public string GameTimer => _gameTimer;
        public string ProcessTimer => _processTimer;
        public float ScoreRequiredForAISkip => _scoreRequiredForAISkip;
        public float ScoreGatheredForAISkip => _scoreGatheredForAISkip;

        public bool FirstTimePlaying
        {
            get => _firstTimePlaying;
            set => _firstTimePlaying = value;
        }

        public string CurrentGameState
        {
            get { return _currentGameState; }
            set { _currentGameState = value; }
        }

        public bool IsAiSkipReady
        {
            get { return _isAISkipReady; }
            set { _isAISkipReady = value; }
        }

        public string CurrentProduct
        {
            get { return _currentProduct; }
            set { _currentProduct = value; }
        }

        // Keys for PlayerPrefs
        private readonly string gameLengthInSecondsKey = "GameLengthInSeconds";
        private readonly string processLengthInSecondsKey = "ProcessLengthInSeconds";
        private readonly string gameScoreKey = "GameScore";
        private readonly string isAiSkipReadyKey = "IsAiSkipReady";
        private readonly string aiSkipGatheredScoreKey = "AiSkipGatheredScore";
        private readonly string currentGameStateKey = "currentGameState";
        private readonly string currentProductKey = "CurrentProduct";
        private readonly string firstTimePlayingKey = "FirstTimePlaying";

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

            LoadSaveData();
        }

        private void Update()
        {
            int minutes = Mathf.FloorToInt(_gameLengthInSeconds / 60);
            int seconds = Mathf.FloorToInt(_gameLengthInSeconds % 60);
            _gameTimer = string.Format("{0}:{1:00}", minutes, seconds);
            int processMinutes = Mathf.FloorToInt(_processLengthInSeconds / 60);
            int processSeconds = Mathf.FloorToInt(_processLengthInSeconds % 60);
            _processTimer = string.Format("{0}:{1:00}", processMinutes, processSeconds);

            if (SceneManager.GetActiveScene().name != "Title" && _currentGameState != "ProductSelection")
            {
                _gameLengthInSeconds -= Time.deltaTime;
            }

            if (SceneManager.GetActiveScene().name == "Factory" && _currentGameState != "ProductSelection")
            {
                _processLengthInSeconds -= Time.deltaTime;
            }

            if (_processLengthInSeconds <= 0f && SceneManager.GetActiveScene().name == "Factory")
            {
                AddToGameScore(_scorefromWaitingOutMinigame);
                ChangeActiveMiniGame();
                _processLengthInSeconds = _originalProcessLength;
                FindObjectOfType<UIManager>().EnablePlayButtonOrProductSelection();
            }
        }

        public void AddToGameScore(float minigameScore)
        {
            _gameScore += minigameScore;
        }

        public void AddAiSkipProgress(float minigameScore)
        {
            if (_currentProduct == "Product2")
            {
                minigameScore /= 2; // Product2 doubles points but ai skip progressing speed stays the same
            }

            _scoreGatheredForAISkip += minigameScore;

            if (_scoreGatheredForAISkip >= _scoreRequiredForAISkip && _isAISkipReady == false)
            {
                _isAISkipReady = true;
                _scoreGatheredForAISkip = 0;
            }

            else if (_isAISkipReady == true) // AISkip has to be used before you can gather score towards it again
            {
                _scoreGatheredForAISkip = 0;
            }
        }

        public void ChangeActiveMiniGame()
        {
            _processLengthInSeconds = _originalProcessLength;

            if (_currentGameState == "Minigame1")
            {
                _currentGameState = "Minigame2";
            }
            else if (_currentGameState == "Minigame2")
            {
                _currentGameState = "ProductSelection";
            }
            else if (_currentGameState == "ProductSelection")
            {
                _currentGameState = "Minigame1";
            }
        }

        private void OnApplicationPause()
        {
            SaveGameData();
        }

        private void OnApplicationQuit()
        {
            SaveGameData();
        }

        public void SaveGameData()
        {
            PlayerPrefs.SetFloat(gameLengthInSecondsKey, _gameLengthInSeconds);
            PlayerPrefs.SetFloat(processLengthInSecondsKey, _processLengthInSeconds);
            PlayerPrefs.SetFloat(gameScoreKey, _gameScore);
            PlayerPrefs.SetInt(isAiSkipReadyKey, _isAISkipReady ? 1 : 0);
            PlayerPrefs.SetFloat(aiSkipGatheredScoreKey, _scoreGatheredForAISkip);
            PlayerPrefs.SetString(currentGameStateKey, _currentGameState);
            PlayerPrefs.SetString(currentProductKey, _currentProduct);
            PlayerPrefs.SetInt(firstTimePlayingKey, _firstTimePlaying ? 1 : 0);

        }

        private void LoadSaveData()
        {
            _gameScore = PlayerPrefs.GetFloat(gameScoreKey, _gameScore);
            _gameLengthInSeconds = PlayerPrefs.GetFloat(gameLengthInSecondsKey, _gameLengthInSeconds);
            _processLengthInSeconds = PlayerPrefs.GetFloat(processLengthInSecondsKey, _processLengthInSeconds);
            _isAISkipReady = PlayerPrefs.GetInt(isAiSkipReadyKey, 0) == 1;
            _scoreGatheredForAISkip = PlayerPrefs.GetFloat(aiSkipGatheredScoreKey, _scoreGatheredForAISkip);
            _currentGameState = PlayerPrefs.GetString(currentGameStateKey, "ProductSelection");
            _currentProduct = PlayerPrefs.GetString(currentProductKey, "Product1");
            _firstTimePlaying = PlayerPrefs.GetInt(firstTimePlayingKey, 1) == 1;

        }

        public void ResetSaveData()
        {
            PlayerPrefs.DeleteKey(gameLengthInSecondsKey);
            PlayerPrefs.DeleteKey(processLengthInSecondsKey);
            PlayerPrefs.DeleteKey(gameScoreKey);
            PlayerPrefs.DeleteKey(isAiSkipReadyKey);
            PlayerPrefs.DeleteKey(aiSkipGatheredScoreKey);
            PlayerPrefs.DeleteKey(currentGameStateKey);
            PlayerPrefs.Save();
            Destroy(gameObject);
        }
    }
}
