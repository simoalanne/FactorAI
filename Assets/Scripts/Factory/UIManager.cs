using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using Global;
using UnityEngine.Localization;
using System;
using UnityEngine.Localization.Tables;


namespace Factory
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button _playButton1;
        [SerializeField] private Button _playButton2;
        [SerializeField] private Button _aiSkipButton;
        [SerializeField] private Button _aiSkipInfoButton;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _processTimerText;
        [SerializeField] private TMP_Text _aiSkipInfoText;
        [SerializeField] private float _scoreFromAiSkip = 50000f;
        [SerializeField] private GameObject _gameEndMenu;

        private void Start()
        {
            if (GameManager.Instance == null) return;

            EnablePlayButton();

            if (GameManager.Instance.IsAiSkipReady)
            {
                _aiSkipButton.interactable = true;
                _aiSkipButton.GetComponent<ButtonAnimation>().enabled = true;
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
            Debug.Log(LocalizationSettings.SelectedLocale.name);
            Debug.Log(LocalizationSettings.SelectedLocale.LocaleName);

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
            _aiSkipButton.GetComponent<ButtonAnimation>().enabled = false;
            GameManager.Instance.AddToGameScore(_scoreFromAiSkip);
            GameManager.Instance.ChangeActiveMiniGame();
            if (_aiSkipInfoText.enabled)
            {
                _aiSkipInfoText.enabled = false;
            }
        }

        public void EnablePlayButton()
        {
            if (GameManager.Instance.ActiveMiniGameName == "Minigame1")
            {
                _playButton1.interactable = true;
                _playButton2.interactable = false;

                _playButton1.GetComponent<ButtonAnimation>().enabled = true;
                _playButton2.GetComponent<ButtonAnimation>().enabled = false;
            }

            else if (GameManager.Instance.ActiveMiniGameName == "Minigame2")
            {
                _playButton1.interactable = false;
                _playButton2.interactable = true;

                _playButton1.GetComponent<ButtonAnimation>().enabled = false;
                _playButton2.GetComponent<ButtonAnimation>().enabled = true;
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
                if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("en"))
                {
                    _aiSkipInfoText.text += "\n Ready!";
                }
                else
                {
                    _aiSkipInfoText.text += "\n Valmis!";
                }

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

            _aiSkipInfoText.text += "\n" + Mathf.FloorToInt(GameManager.Instance.ScoreGatheredForAISkip / GameManager.Instance.ScoreRequiredForAISkip * 100) + " / 100%";
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
