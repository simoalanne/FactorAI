using Global;
using UnityEngine;
using Audio;

namespace Minigame1
{
    public class GameStatsManager : MonoBehaviour
    {

        [SerializeField] float _miniGameLengthInSeconds = 60.0f;
        [SerializeField] int _minCompletedProducts = 3;

        private SoundEffectPlayer _soundEffectPlayer;
        private OnMinigameEnd _onMinigameEnd;
        private string _minigameTime;
        private float _score;
        private int _completedProducts;
        private bool _gameActive = true;
        private ScorePopUp _scorePopUp;
        private int _scoreMultiplier = 1;

        public float Score => _score;
        public string MinigameTime => _minigameTime;
        public int MinCompletedProducts => _minCompletedProducts;
        public int CompletedProducts => _completedProducts;

        void Awake()
        {
            _soundEffectPlayer = GetComponent<SoundEffectPlayer>();

            if (GameManager.Instance.CurrentProduct == "Product1" || GameManager.Instance == null)
            {
                _minCompletedProducts = 3;
            }
            else if (GameManager.Instance.CurrentProduct == "Product2")
            {
                _minCompletedProducts = 6;
                _scoreMultiplier = 2;
            }
        }
        void Start()
        {
            _onMinigameEnd = GetComponent<OnMinigameEnd>();
            _scorePopUp = GetComponent<ScorePopUp>();

        }

        void Update()
        {
            if (_miniGameLengthInSeconds <= 0f && _gameActive)
            {
                _gameActive = false;
                CheckGameEnd();
                return;
            }

            _miniGameLengthInSeconds -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(_miniGameLengthInSeconds % 60);
            float fraction = _miniGameLengthInSeconds * 100 % 100;
            _minigameTime = string.Format("{0}:{1:00}", seconds, Mathf.FloorToInt(fraction));
        }

        private void CheckGameEnd()
        {
            if (_completedProducts >= _minCompletedProducts)
            {
                _onMinigameEnd.OnGameWon(_score);
            }
            else
            {
                _onMinigameEnd.OnGameLost();
            }

            DragAndDrop[] dragAndDropInstances = FindObjectsOfType<DragAndDrop>();
            foreach (DragAndDrop instance in dragAndDropInstances)
            {
                instance.enabled = false;
            }
        }

        public void IncreaseScore(float score, Transform transform)
        {
            score *= _scoreMultiplier;
            _score += score;
            _scorePopUp.ShowPopUp(transform.position, score);
            _soundEffectPlayer.PlaySoundEffect(0);
        }

        public void IncreaseCompletedProducts()
        {
            _completedProducts++;
        }
    }
}
