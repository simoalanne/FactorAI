using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Global;

namespace Factory
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _processTimerText;
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
            _processTimerText.text = GameManager.Instance.ActiveMiniGameName + " Process Time: \n" + GameManager.Instance.ProcessTimer;
        }

        public void LoadNextMinigame()
        {
            SceneManager.LoadSceneAsync(GameManager.Instance.ActiveMiniGameName);
        }

        public void UseAISkip()
        {
            GameManager.Instance.IsAiSkipReady = false;
            _aiSkipButton.interactable = false;
            GameManager.Instance.AddToGameScore(_scoreFromAiSkip);
            GameManager.Instance.ChangeActiveMiniGame();
        }

        public void DisplayProcessTimer()
        {
            if (_processTimerText.enabled)
            {
                _processTimerText.enabled = false;
            }
            else
            {
                _processTimerText.enabled = true;
            }
        }
    }
}
