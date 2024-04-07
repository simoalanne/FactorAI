using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using Global;

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
        [SerializeField] private GameObject _processTimerAndInfo;
        [SerializeField] private GameObject _processLine1;
        [SerializeField] private GameObject _processLine2;
        [SerializeField] private Sprite _playButtonIconEnglish;
        [SerializeField] private Sprite _playButtonIconFinnish;



        private void Start()
        {
            if (GameManager.Instance == null) return;

            Debug.Log(LocalizationSettings.SelectedLocale);

            if (LocalizationSettings.SelectedLocale.Identifier == LocalizationSettings.AvailableLocales.GetLocale("en").Identifier)
            {
                _playButton1.GetComponent<Image>().sprite = _playButtonIconEnglish;
                _playButton2.GetComponent<Image>().sprite = _playButtonIconEnglish;
            }
            else if (LocalizationSettings.SelectedLocale.Identifier == LocalizationSettings.AvailableLocales.GetLocale("fi").Identifier)
            {
                _playButton1.GetComponent<Image>().sprite = _playButtonIconFinnish;
                _playButton2.GetComponent<Image>().sprite = _playButtonIconFinnish;
            }

            EnablePlayButton();

            _processTimerText.enabled = false;
            _gameEndMenu.SetActive(false);
            _aiSkipInfoText.enabled = false;
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
            _scoreText.text = GameManager.Instance.GameScore.ToString();
            _timerText.text = GameManager.Instance.GameTimer;
            _processTimerText.text = GameManager.Instance.ProcessTimer;

            if (GameManager.Instance.GameLengthInSeconds <= 60f)
            {
                _timerText.color = Color.red;
            }
            else
            {
                _timerText.color = Color.white;
            }

            if (GameManager.Instance.GameLengthInSeconds <= 0f)
            {
                _timerText.text = "00:00";
            }
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
            EnablePlayButton();
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
                _processTimerAndInfo.transform.SetParent(_processLine1.transform);
                _processTimerAndInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 150);
            }

            else if (GameManager.Instance.ActiveMiniGameName == "Minigame2")
            {
                _playButton1.interactable = false;
                _playButton2.interactable = true;

                _playButton1.GetComponent<ButtonAnimation>().enabled = false;
                _playButton2.GetComponent<ButtonAnimation>().enabled = true;
                _processTimerAndInfo.transform.SetParent(_processLine2.transform);
                _processTimerAndInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 150); // :D
            }

            CheckAISkipStatus();
        }

        public void CheckAISkipStatus()
        {
            if (GameManager.Instance.IsAiSkipReady)
            {
                _aiSkipButton.interactable = true;
                _aiSkipButton.GetComponent<ButtonAnimation>().enabled = true;

                if (_aiSkipInfoText.enabled)
                {
                    _aiSkipInfoText.enabled = false;
                    _aiSkipInfoText.enabled = false;

                }
            }
            else
            {
                _aiSkipButton.interactable = false;
                _aiSkipButton.GetComponent<ButtonAnimation>().enabled = false;
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
                if (LocalizationSettings.SelectedLocale.Identifier == LocalizationSettings.AvailableLocales.GetLocale("en").Identifier)
                {
                    _aiSkipInfoText.text = "Ready!";
                }
                else if (LocalizationSettings.SelectedLocale.Identifier == LocalizationSettings.AvailableLocales.GetLocale("fi").Identifier)
                {
                    _aiSkipInfoText.text = "Valmis!";
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

            _aiSkipInfoText.text = Mathf.FloorToInt(GameManager.Instance.ScoreGatheredForAISkip / GameManager.Instance.ScoreRequiredForAISkip * 100) + "/100%";
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
