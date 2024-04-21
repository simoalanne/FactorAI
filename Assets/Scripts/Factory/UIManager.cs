using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using Global;
using Audio;

namespace Factory
{
    public class UIManager : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _playButton1;
        [SerializeField] private Button _playButton2;
        [SerializeField] private Button _aiSkipButton;
        [SerializeField] private Button _aiSkipInfoButton;
        [SerializeField] private Button _confirmProductChoiceButton;

        [Header("Texts")]
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _processTimerText;
        [SerializeField] private TMP_Text _aiSkipInfoText;

        [Header("Sprites")]
        [SerializeField] private Sprite _playButtonIconEnglish;
        [SerializeField] private Sprite _playButtonIconFinnish;

        [Header("Canvases and Panels")]
        [SerializeField] private GameObject _gameEndMenu;
        [SerializeField] private GameObject _productSelectionMenu;
        [SerializeField] private GameObject _tutorialUI;
        [SerializeField] private GameObject _processTimerAndInfo;

        [Header("Animated Product Lines")]
        [SerializeField] private GameObject _processLine1;
        [SerializeField] private GameObject _processLine2;

        [Header("Misc")]
        [SerializeField] private float _scoreFromAiSkip = 100000f;

        private SoundEffectPlayer _soundEffectPlayer;
        private bool _colorSwitched = false;
        private string _currentlySelectedProduct = "None";



        private void Awake()
        {
            if (GameManager.Instance == null) return;

            _soundEffectPlayer = GetComponent<SoundEffectPlayer>();

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

            _processTimerText.enabled = false;
            _gameEndMenu.SetActive(false);
            _aiSkipInfoText.enabled = false;
            _confirmProductChoiceButton.interactable = false;

            if (GameManager.Instance.FirstTimePlaying)
            {
                _tutorialUI.GetComponent<Tutorial.GameTutorial>().StartTutorial();
                return; // Dont activate the play button before the tutorial is done for the first time.
            }

            EnablePlayButtonOrProductSelection();


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

            if (GameManager.Instance.GameLengthInSeconds <= 60f && !_colorSwitched)
            {
                _timerText.color = new Color(196f / 255f, 44f / 255f, 54f / 255f); // Red color in the palette
            }

            if (GameManager.Instance.GameLengthInSeconds <= 0f)
            {
                _timerText.text = "00:00";
            }
        }

        public void LoadNextMinigame()
        {
            if (GameManager.Instance.CurrentGameState == "ProductSelection")
            {
                Debug.LogError("Product selection is not a minigame!");
                return;
            }

            SceneManager.LoadSceneAsync(GameManager.Instance.CurrentGameState);
        }

        public void UseAISkip()
        {
            GameManager.Instance.IsAiSkipReady = false;
            _aiSkipButton.interactable = false;
            _aiSkipButton.GetComponent<ButtonAnimation>().enabled = false;
            GameManager.Instance.AddToGameScore(_scoreFromAiSkip);
            GameManager.Instance.ChangeActiveMiniGame();
            EnablePlayButtonOrProductSelection();
            if (_aiSkipInfoText.enabled)
            {
                _aiSkipInfoText.enabled = false;
            }
        }

        public void EnablePlayButtonOrProductSelection()
        {
            if (GameManager.Instance.CurrentGameState == "Minigame1")
            {
                _playButton1.interactable = true;
                _playButton2.interactable = false;

                _playButton1.GetComponent<ButtonAnimation>().enabled = true;
                _playButton2.GetComponent<ButtonAnimation>().enabled = false;
                _processTimerAndInfo.transform.SetParent(_processLine1.transform); // Sets the process timer and info to be a child of the correct process line.
                _processTimerAndInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 150); // Sets the position of the process timer and info.
            }

            else if (GameManager.Instance.CurrentGameState == "Minigame2")
            {
                _playButton1.interactable = false;
                _playButton2.interactable = true;

                _playButton1.GetComponent<ButtonAnimation>().enabled = false;
                _playButton2.GetComponent<ButtonAnimation>().enabled = true;
                _processTimerAndInfo.transform.SetParent(_processLine2.transform);
                _processTimerAndInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 150); // Sets the position of the process timer and info.
            }

            else if (GameManager.Instance.CurrentGameState == "ProductSelection")
            {
                _playButton1.interactable = false;
                _playButton2.interactable = false;

                _playButton1.GetComponent<ButtonAnimation>().enabled = false;
                _playButton2.GetComponent<ButtonAnimation>().enabled = false;

                Debug.Log("Product selection menu enabled");
                _productSelectionMenu.SetActive(true);

            }

            CheckAISkipStatus();
        }

        public void SelectProduct1()
        {
            if (_currentlySelectedProduct == "Product1")
            {
                _currentlySelectedProduct = "None";
                GameObject.Find("Product1Button").GetComponent<ButtonAnimation>().enabled = false;
                _confirmProductChoiceButton.interactable = false;
                _confirmProductChoiceButton.GetComponentInChildren<TMP_Text>().color = _confirmProductChoiceButton.colors.disabledColor;
            }
            else
            {
                _currentlySelectedProduct = "Product1";
                GameObject.Find("Product1Button").GetComponent<ButtonAnimation>().enabled = true;
                GameObject.Find("Product2Button").GetComponent<ButtonAnimation>().enabled = false;
                _confirmProductChoiceButton.interactable = true;
                _confirmProductChoiceButton.GetComponentInChildren<TMP_Text>().color = _confirmProductChoiceButton.colors.normalColor;

            }
        }

        public void SelectProduct2()
        {
            if (_currentlySelectedProduct == "Product2")
            {
                _currentlySelectedProduct = "None";
                GameObject.Find("Product2Button").GetComponent<ButtonAnimation>().enabled = false;
                _confirmProductChoiceButton.interactable = false;
                _confirmProductChoiceButton.GetComponentInChildren<TMP_Text>().color = _confirmProductChoiceButton.colors.disabledColor;
            }
            else
            {
                _currentlySelectedProduct = "Product2";
                GameObject.Find("Product2Button").GetComponent<ButtonAnimation>().enabled = true;
                GameObject.Find("Product1Button").GetComponent<ButtonAnimation>().enabled = false;
                _confirmProductChoiceButton.interactable = true;
                _confirmProductChoiceButton.GetComponentInChildren<TMP_Text>().color = _confirmProductChoiceButton.colors.normalColor;
            }
        }

        public void ConfirmProductChoice()
        {
            GameManager.Instance.CurrentProduct = _currentlySelectedProduct;
            _productSelectionMenu.SetActive(false);
            GameManager.Instance.ChangeActiveMiniGame();
            EnablePlayButtonOrProductSelection();
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
