using Global;
using UnityEngine;

namespace Minigame2
{
    public class GameStatsManager : MonoBehaviour
    {
        [SerializeField] int _howManyForWin = 20;
        [SerializeField] int _maxFails = 3;
        [SerializeField] private float _scorePenaltyFromFail = 5000;

        private OnMinigameEnd _onMinigameEnd;
        private float _score;
        private int _collected;
        private int _fails;
        private string _minigameTime;
        private bool _gameActive = true;

        public string MinigameTime => _minigameTime;
        public float Score => _score;
        public int Collected => _collected;
        public int HowManyForWin => _howManyForWin;
        public int Fails => _fails;
        public int MaxFails => _maxFails;

        private void Start()
        {
            _onMinigameEnd = GetComponent<OnMinigameEnd>();
        }

        public void UpdateStats(bool Scored)
        {
            if (_gameActive == false) return;

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
                _gameActive = false;
            }
            else if (_fails >= _maxFails)
            {
                _onMinigameEnd.OnGameLost();
                _gameActive = false;
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
