using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGame2
{
    public class GameStatsManager : MonoBehaviour
    {
        [SerializeField] float _miniGameLenghtInSeconds = 90.0f;
        [SerializeField] int _howManyForWin = 15;
        [SerializeField] int _maxFails = 3;
        [SerializeField] string _sceneToLoad;
        private int _score;
        private int _fails;
        private float _elapsedTime;
        private string _printTime;

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(_elapsedTime / 60);
            int seconds = Mathf.FloorToInt(_elapsedTime % 60);
            _printTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            
            if (_elapsedTime >= _miniGameLenghtInSeconds)
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

            if (_fails >= _maxFails || _score >= _howManyForWin)
            {
                EndGame();
            }

        }

        private void IncreaseScore()
        {
            _score++;
            Debug.Log("Score: " + _score);
            // TODO: Add score to UI and play sound/animation
        }

        private void IncreaseFails()
        {
            _fails++;
            Debug.Log("Fails: " + _fails);
            // TODO: Add fails to UI and play sound/animation
        }

        private void EndGame()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}
