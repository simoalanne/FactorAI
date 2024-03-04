using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGame2
{
    public class GameStatsManager : MonoBehaviour
    {
        [SerializeField] float _miniGameLengthInSeconds = 90.0f;
        [SerializeField] int _howManyForWin = 30;
        [SerializeField] int _maxFails = 5;
        [SerializeField] string _sceneToLoad;
        private int _score;
        private int _collected;
        private int _fails;
        private string _printTime;

        public string PrintTime => _printTime;
        public int Score => _score;
        public int Fails => _fails;

        private void Update()
        {
            _miniGameLengthInSeconds -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(_miniGameLengthInSeconds / 60);
            int seconds = Mathf.FloorToInt(_miniGameLengthInSeconds % 60);
            _printTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (_miniGameLengthInSeconds <= 0.0f)
            {
                EndGame();
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

            if (_fails >= _maxFails || _collected >= _howManyForWin)
            {
                EndGame();
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
            // TODO: Add fails to UI and play sound/animation
        }

        private void EndGame()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}
