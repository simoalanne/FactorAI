using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGame1
{
    public class ScoreManager : MonoBehaviour
    {
        private int score = 0;

        private void Start()
        {
            // Load the score from PlayerPrefs when the scene starts
            LoadScore();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Correct"))
            {
                // Increase score if collided with "Correct" object
                IncreaseScore();
            }

            // Save the score before loading the next scene
            SaveScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void IncreaseScore()
        {
            score++;
            Debug.Log("Score: " + score);
        }

        private void SaveScore()
        {
            // Save the score to PlayerPrefs
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.Save();
        }

        private void LoadScore()
        {
            // Load the score from PlayerPrefs
            score = PlayerPrefs.GetInt("Score", 0);
        }
    }
}
