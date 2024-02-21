using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGame2
{
    public class GameStatsManager : MonoBehaviour
    {
        [SerializeField] int _maxFails = 3;
        [SerializeField] int _howManyRounds = 5;
        private int _score = 0;
        private int _fails = 0;
        private int _roundsPlayed = 0;

        private void Start()
        {
            LoadStats();
        }

        // can be accessed by scripts in the same namespace
        internal void UpdateStats(bool Scored)
        {
            IncreaseRoundsPlayed();

            if (Scored)
            {
                IncreaseScore();
            }
            else if (!Scored)
            {
                IncreaseFails();
            }

            if (_fails >= _maxFails || _roundsPlayed >= _howManyRounds)
            {
                EndGame();
            }
            else
            {
                ContinueGame();
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

        private void IncreaseRoundsPlayed()
        {
            _roundsPlayed++;
            Debug.Log("Rounds Played: " + _roundsPlayed);
        }

        private void SaveStats()
        {
            PlayerPrefs.SetInt("RoundsPlayed", _roundsPlayed);
            PlayerPrefs.SetInt("Score", _score);
            PlayerPrefs.SetInt("Fails", _fails);
            PlayerPrefs.Save();
        }

        private void LoadStats()
        {
            _roundsPlayed = PlayerPrefs.GetInt("RoundsPlayed", 0);
            _score = PlayerPrefs.GetInt("Score", 0);
            _fails = PlayerPrefs.GetInt("Fails", 0);

        }

        private void ContinueGame()
        {
            SaveStats();
            SceneManager.LoadScene("MiniGame2");
        }

        private void EndGame()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Menu");
        }
    }
}
