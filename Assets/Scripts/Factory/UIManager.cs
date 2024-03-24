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
        [SerializeField] private float _scoreFromAiSkip = 50000;
        [SerializeField] private Button _aiSkipInfoButton;
        [SerializeField] private TMP_Text _aiSkipInfoText;
        [SerializeField] private GameObject _gameEndMenu;

        private void Start()
        {
            if (GameManager.Instance.IsAiSkipReady)
            {
                _aiSkipButton.interactable = true;
            }

            _processTimerText.enabled = false;
            _gameEndMenu.SetActive(false);
            _aiSkipInfoText.enabled = false;
        }

        private void Update()
        {
            UpdateStats();

            if (GameManager.Instance.GameLengthInSeconds <= 0f)
            {
                InitGameEnd();
            }
        }

        private void InitGameEnd()
        {
            _gameEndMenu.SetActive(true);
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
            if (_aiSkipInfoText.enabled)
            {
                _aiSkipInfoText.enabled = false;
            }
        }

        public void DisplayAISkipStatus()
        {
            if (_aiSkipInfoText.enabled)
            {
                _aiSkipInfoText.enabled = false;
                return;
            }

            _aiSkipInfoText.enabled = true;

            if (_aiSkipButton.interactable)
            {
                _aiSkipInfoText.text = "Status: Ready";
                _aiSkipInfoText.color = Color.green;
                return;
            }

            if (GameManager.Instance.ScoreGatheredForAISkip / GameManager.Instance.ScoreRequiredForAISkip < 0.5)
            {
                _aiSkipInfoText.color = Color.red;
            }
            else
            {
                _aiSkipInfoText.color = new Color(1f, 0.5f, 0f); // RGB for orange
            }

            _aiSkipInfoText.text = "Status: \n" + GameManager.Instance.ScoreGatheredForAISkip / GameManager.Instance.ScoreRequiredForAISkip * 100 + " / 100%";
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
