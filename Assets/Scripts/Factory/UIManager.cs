using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Global;
using UnityEngine.UI;

namespace Factory
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _aiSkipButton;
        [SerializeField] private float _scoreFromAiSkip = 50000f;

        private void Start()
        {
            if (GameManager.Instance.IsAiSkipReady)
            {
                _aiSkipButton.interactable = true;
            }
        }

        private void Update()
        {
            UpdateStats();
        }

        private void UpdateStats()
        {
            if (_scoreText == null || _timerText == null || GameManager.Instance == null) return;
            _scoreText.text = "Score: \n" + GameManager.Instance.GameScore;
            _timerText.text = "Time: \n" + GameManager.Instance.GameTimer;
        }

        public void LoadNextMinigame()
        {
            SceneManager.LoadSceneAsync(GameManager.Instance.MiniGameName);
        }

        public void UseAISkip()
        {
            GameManager.Instance.IsAiSkipReady = false;
            _aiSkipButton.interactable = false;
            GameManager.Instance.AddToGameScore(_scoreFromAiSkip);

            if (GameManager.Instance.MiniGameName == "Minigame1")
            {
                GameManager.Instance.MiniGameName = "Minigame2";
            }
            else
            {
                GameManager.Instance.MiniGameName = "Minigame1";
            }
        }
    }
}
