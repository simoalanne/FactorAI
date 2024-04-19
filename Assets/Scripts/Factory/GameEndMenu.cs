using TMPro;
using UnityEngine;
using Global;
using UnityEngine.SceneManagement;

namespace Factory
{
    public class GameEndMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text _newHighScoreText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private float _scorePerInvoke = 100f;
        [SerializeField] private float _scoreCountDownRate = 0.05f;
        private float _scoreCountDown = 0f;

        private void Awake()
        {
            if (GameManager.Instance.GameScore <= 0)
            {
                _scoreText.text = "0";
                GameManager.Instance.ResetSaveData();
                return;
            }

            HighScoreManager.Instance.CheckForHighScore(GameManager.Instance.GameScore);
            GameManager.Instance.ResetSaveData();

            InvokeRepeating(nameof(UpdateScore), 0f, _scoreCountDownRate);
        }

        public void BackToTitle()
        {
            SceneManager.LoadSceneAsync("Title");
        }

        private void UpdateScore()
        {
            if (_scoreCountDown < GameManager.Instance.GameScore)
            {
                _scoreCountDown += _scorePerInvoke;
                _scoreText.text = _scoreCountDown.ToString();
            }
            else
            {
                if (HighScoreManager.Instance.NewHighScore)
                {
                    _newHighScoreText.enabled = true;
                }

                CancelInvoke(nameof(UpdateScore));
            }
        }
    }
}
