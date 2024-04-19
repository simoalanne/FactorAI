using Global;
using UnityEngine;

namespace Minigame2
{
    public class GameStatsManager : MonoBehaviour
    {
        [SerializeField] int _howManyForWin = 20;
        [SerializeField] int _maxFails = 3;
        [SerializeField] private float _scoreFromCatch = 2000f;
        [SerializeField] private float _scorePenaltyFromFail = -5000f;

        private ScorePopUp _scorePopUp;
        private OnMinigameEnd _onMinigameEnd;
        private float _score;
        private int _collected;
        private int _fails;
        private bool _gameActive = true;

        public float Score => _score;
        public int Collected => _collected;
        public int HowManyForWin => _howManyForWin;
        public int Fails => _fails;
        public int MaxFails => _maxFails;

        private void Start()
        {
            _scorePopUp = GetComponent<ScorePopUp>();
            _onMinigameEnd = GetComponent<OnMinigameEnd>();

            if (GameManager.Instance.CurrentProduct == "Product2")
            {
                _maxFails -= 2;
                _scoreFromCatch *= 2f;
                _scorePenaltyFromFail *= 2f;
            }
        }

        public void UpdateStats(bool scored, Vector2 collisionPosition)
        {
            if (_gameActive == false) return;


            if (scored)
            {
                _scorePopUp.ShowPopUp(collisionPosition, _scoreFromCatch);
                IncreaseScore();
            }
            else
            {
                _scorePopUp.ShowPopUp(collisionPosition, _scorePenaltyFromFail);
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
            _score += _scoreFromCatch;

        }

        private void IncreaseFails()
        {
            _fails++;
            if (_score + _scorePenaltyFromFail >= 0)
            {
                _score += _scorePenaltyFromFail;
            }
            else
            {
                _score = 0;
            }

        }
    }
}
