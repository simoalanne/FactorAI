using Global;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigame2
{
    public class GameStatsManager : MonoBehaviour
    {
        [SerializeField] float _miniGameLengthInSeconds = 60.0f;
        [SerializeField] int _howManyForWin = 20;
        [SerializeField] int _maxFails = 3;
        [SerializeField] private int _scorePenaltyFromFail = 5000;

        private OnMinigameEnd _onMinigameEnd;
        private int _score;
        private int _collected;
        private int _fails;
        private string _minigameTime;

        public string MinigameTime => _minigameTime;
        public int Score => _score;
        public int Collected => _collected;
        public int HowManyForWin => _howManyForWin;
        public int Fails => _fails;
        public int MaxFails => _maxFails;

        private void Start()
        {
            _onMinigameEnd = GetComponent<OnMinigameEnd>();
        }

        private void Update()
        {
            _miniGameLengthInSeconds -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(_miniGameLengthInSeconds / 60);
            int seconds = Mathf.FloorToInt(_miniGameLengthInSeconds % 60);
            float fraction = _miniGameLengthInSeconds * 100 % 100;
            _minigameTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, Mathf.FloorToInt(fraction));

            if (_miniGameLengthInSeconds <= 0.0f)
            {
                _minigameTime = "00:00:00";
                _onMinigameEnd.OnGameLost();
            }
        }

        public void UpdateStats(bool Scored)
        {
            if (Scored)
            {
                IncreaseScore();
            }
            else
            {
                IncreaseFails();
            }

            if (_collected >= _howManyForWin)
            {
                _onMinigameEnd.OnGameWon(_score);
            }
            else if (_fails >= _maxFails)
            {
                _onMinigameEnd.OnGameLost();
            }
        }

        private void IncreaseScore()
        {
            _collected++;
            _score += 2000;

            // TODO: Add score to UI and play sound/animation
        }

        private void IncreaseFails()
        {
            _fails++;
            _score -= _scorePenaltyFromFail;
            // TODO: Add fails to UI and play sound/animation
        }
    }
}
