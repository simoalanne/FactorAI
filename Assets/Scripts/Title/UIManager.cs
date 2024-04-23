using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;
using Audio;


namespace Title
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI Panels and Canvases")]
        [SerializeField] private GameObject _titleMenu;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _highScorePanel;
        [SerializeField] private GameObject _creditsPanel;

        [Header("Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _toggleMusicButton;
        [SerializeField] private Button _toggleAudioButton;
        [SerializeField] private Button _resetHighScoresButton;


        [Header("Sprites")]

        [SerializeField] private Sprite _playButtonIconEnglish;
        [SerializeField] private Sprite _pelaaButtonIconFinnish;

        [SerializeField] private Sprite _musicOnIcon;
        [SerializeField] private Sprite _musicOffIcon;

        [SerializeField] private Sprite _audioOnIcon;
        [SerializeField] private Sprite _audioOffIcon;


        [Header("Locales")]

        [SerializeField] private Locale _englishLocale;
        [SerializeField] private Locale _finnishLocale;

        [Header("Texts")]

        [SerializeField] private TMP_Text _playerNameText1;
        [SerializeField] private TMP_Text _playerNameText2;
        [SerializeField] private TMP_Text _playerNameText3;
        [SerializeField] private TMP_Text _highScoreText1;
        [SerializeField] private TMP_Text _highScoreText2;
        [SerializeField] private TMP_Text _highScoreText3;

        [Header("SceneName")]
        [SerializeField] private string _sceneToLoad;

        private Image _languageIcon;
        private Image _musicIcon;
        private Image _audioIcon;

        private string _currentLanguage = "english";
        private bool _musicEnabled = true;
        private bool _audioEnabled = true;

        private string _originalCurrentLanguage;
        private bool _originalMusicEnabled;
        private bool _originalAudioEnabled;

        private readonly string _currentLanguageKey = "CurrentLanguage";
        private readonly string _musicEnabledKey = "MusicEnabled";
        private readonly string _audioEnabledKey = "AudioEnabled";

        private void Start()
        {
            _titleMenu.SetActive(true);
            _settingsPanel.SetActive(false);
            _highScorePanel.SetActive(false);
            _creditsPanel.SetActive(false);

            _musicIcon = _toggleMusicButton.transform.Find("MusicIcon").GetComponent<Image>();
            _audioIcon = _toggleAudioButton.transform.Find("AudioIcon").GetComponent<Image>();

            LoadSettingPreferences();
            Debug.Log("music: " + _musicEnabled);
            Debug.Log("audio: " + _audioEnabled);
            Debug.Log("language: " + _currentLanguage);

            if (PlayerPrefs.GetString("CurrentLanguage").Equals("english"))
            {
                LocalizationSettings.SelectedLocale = _englishLocale;
            }
            else if (PlayerPrefs.GetString("CurrentLanguage").Equals("finnish"))
            {
                LocalizationSettings.SelectedLocale = _finnishLocale;
            }

            if (HighScoreManager.Instance.HighScores.Count > 0)
            {
                _resetHighScoresButton.interactable = true;
            }
            else
            {
                _resetHighScoresButton.interactable = false;
                _resetHighScoresButton.GetComponentInChildren<TMP_Text>().color = new Color(219f / 255f, 224f / 255f, 231f / 255f, 0.5f);
            }

            ChangePlayButtonIcon();

            if (_musicEnabled)
            {
                _musicIcon.sprite = _musicOnIcon;
            }
            else
            {
                _musicIcon.sprite = _musicOffIcon;
            }

            if (_audioEnabled)
            {
                _audioIcon.sprite = _audioOnIcon;
            }
            else
            {
                _audioIcon.sprite = _audioOffIcon;
            }
        }

        private void ChangePlayButtonIcon()
        {
            if (_currentLanguage.Equals("english"))
            {
                _playButton.image.sprite = _playButtonIconEnglish;
            }
            else
            {
                _playButton.image.sprite = _pelaaButtonIconFinnish;
            }
        }

        private void LoadSettingPreferences()
        {
            _currentLanguage = PlayerPrefs.GetString(_currentLanguageKey, "finnish");
            _musicEnabled = PlayerPrefs.GetInt(_musicEnabledKey, 1) == 1;
            _audioEnabled = PlayerPrefs.GetInt(_audioEnabledKey, 1) == 1;

        }

        /*private void OriginalSettingPreferences()
        {
            _originalCurrentLanguage = _currentLanguage;
            _originalTutorialEnabled = _tutorialEnabled;
            _originalMusicEnabled = _musicEnabled;
            _originalAudioEnabled = _audioEnabled;
        } */

        /*private bool SettingsChanged()
        {
            return _originalCurrentLanguage != _currentLanguage ||
                   _originalTutorialEnabled != _tutorialEnabled ||
                   _originalMusicEnabled != _musicEnabled ||
                   _originalAudioEnabled != _audioEnabled;
        }*/

        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(_sceneToLoad);
        }

        public void OpenSettings()
        {
            //OriginalSettingPreferences();
            _titleMenu.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        public void CloseSettings()
        {
            _titleMenu.SetActive(true);
            _settingsPanel.SetActive(false);
            ChangePlayButtonIcon();

            /*if (SettingsChanged())
            {
                Debug.Log("Settings changed");
                SaveSettingPreferences();
            }*/
        }

        public void ChangeToFinnish()
        {
            if (_currentLanguage.Equals("finnish")) return;

            _currentLanguage = "finnish";
            PlayerPrefs.SetString(_currentLanguageKey, _currentLanguage);
            PlayerPrefs.Save();
            LocalizationSettings.SelectedLocale = _finnishLocale;
            ChangePlayButtonIcon();
        }

        public void ChangeToEnglish()
        {
            if (_currentLanguage.Equals("english")) return;

            _currentLanguage = "english";
            PlayerPrefs.SetString(_currentLanguageKey, _currentLanguage);
            PlayerPrefs.Save();
            LocalizationSettings.SelectedLocale = _englishLocale;
            ChangePlayButtonIcon();
        }


        public void ToggleMusic()
        {
            if (_musicEnabled)
            {
                _musicIcon.sprite = _musicOffIcon;
                _musicEnabled = false;
                // TODO: Turn off music
            }
            else
            {
                _musicIcon.sprite = _musicOnIcon;
                _musicEnabled = true;
                // TODO: Add music and turn it on
            }
            PlayerPrefs.SetInt(_musicEnabledKey, _musicEnabled ? 1 : 0);
            PlayerPrefs.Save();
            MusicPlayer.Instance.ToggleMusic();
        }

        public void ToggleAudio()
        {
            if (_audioEnabled)
            {
                _audioIcon.sprite = _audioOffIcon;
                _audioEnabled = false;
                // TODO: Turn off audio
            }
            else
            {
                _audioIcon.sprite = _audioOnIcon;
                _audioEnabled = true;
                // TODO: Add audio and turn it on
            }
            PlayerPrefs.SetInt(_audioEnabledKey, _audioEnabled ? 1 : 0);
            PlayerPrefs.Save();
            MusicPlayer.Instance.ToggleMusic();
        }

        public void ResetHighScores()
        {
            HighScoreManager.Instance.ResetHighScores();
            _resetHighScoresButton.interactable = false;
            _resetHighScoresButton.GetComponentInChildren<TMP_Text>().color = new Color(219f / 255f, 224f / 255f, 231f / 255f, 0.5f);
        }

        public void OpenHighScorePanel()
        {
            WriteHighScores();
            _titleMenu.SetActive(false);
            _highScorePanel.SetActive(true);
        }

        public void CloseHighScorePanel()
        {
            _highScorePanel.SetActive(false);
            _titleMenu.SetActive(true);
        }

        public void OpenCreditsPanel()
        {
            _titleMenu.SetActive(false);
            _creditsPanel.SetActive(true);
        }

        public void CloseCreditsPanel()
        {
            _creditsPanel.SetActive(false);
            _titleMenu.SetActive(true);
        }

        private void WriteHighScores()
        {
            if (HighScoreManager.Instance.HighScores.Count > 0)
            {
                _playerNameText1.text = HighScoreManager.Instance.HighScores[0].PlayerName;
                _highScoreText1.text = HighScoreManager.Instance.HighScores[0].Score.ToString();
            }
            else
            {
                _playerNameText1.text = "";
                _highScoreText1.text = "";
            }

            if (HighScoreManager.Instance.HighScores.Count > 1)
            {
                _playerNameText2.text = HighScoreManager.Instance.HighScores[1].PlayerName;
                _highScoreText2.text = HighScoreManager.Instance.HighScores[1].Score.ToString();
            }
            else
            {
                _playerNameText2.text = "";
                _highScoreText2.text = "";
            }

            if (HighScoreManager.Instance.HighScores.Count > 2)
            {
                _playerNameText3.text = HighScoreManager.Instance.HighScores[2].PlayerName;
                _highScoreText3.text = HighScoreManager.Instance.HighScores[2].Score.ToString();
            }
            else
            {
                _playerNameText3.text = "";
                _highScoreText3.text = "";
            }
        }
    }
}

