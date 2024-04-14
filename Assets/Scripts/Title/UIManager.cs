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

        [Header("Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _changeLanguageButton;
        [SerializeField] private Button _toggleTutorialButton;
        [SerializeField] private Button _toggleMusicButton;
        [SerializeField] private Button _toggleAudioButton;


        [Header("Sprites")]

        [SerializeField] private Sprite _playButtonIconEnglish;
        [SerializeField] private Sprite _pelaaButtonIconFinnish;

        [SerializeField] private Sprite _englishIcon;
        [SerializeField] private Sprite _finnishIcon;

        [SerializeField] private Sprite _tutorialOnIcon;
        [SerializeField] private Sprite _tutorialOffIcon;

        [SerializeField] private Sprite _musicOnIcon;
        [SerializeField] private Sprite _musicOffIcon;

        [SerializeField] private Sprite _audioOnIcon;
        [SerializeField] private Sprite _audioOffIcon;


        [Header("Locales")]

        [SerializeField] private Locale _englishLocale;
        [SerializeField] private Locale _finnishLocale;

        [Header("Texts")]

        [SerializeField] private TMP_Text _highScoreText1;
        [SerializeField] private TMP_Text _highScoreText2;
        [SerializeField] private TMP_Text _highScoreText3;

        [Header("SceneName")]
        [SerializeField] private string _sceneToLoad;

        private Image _languageIcon;
        private Image _tutorialIcon;
        private Image _musicIcon;
        private Image _audioIcon;

        private string _currentLanguage = "english";
        private bool _tutorialEnabled = true;
        private bool _musicEnabled = true;
        private bool _audioEnabled = true;

        private string _originalCurrentLanguage;
        private bool _originalTutorialEnabled;
        private bool _originalMusicEnabled;
        private bool _originalAudioEnabled;

        private readonly string _currentLanguageKey = "CurrentLanguage";
        private readonly string _tutorialEnabledKey = "TutorialEnabled";
        private readonly string _musicEnabledKey = "MusicEnabled";
        private readonly string _audioEnabledKey = "AudioEnabled";

        private void Start()
        {
            _titleMenu.SetActive(true);
            _settingsPanel.SetActive(false);
            _highScorePanel.SetActive(false);

            _languageIcon = _changeLanguageButton.transform.Find("LanguageIcon").GetComponent<Image>();
            _tutorialIcon = _toggleTutorialButton.transform.Find("TutorialIcon").GetComponent<Image>();
            _musicIcon = _toggleMusicButton.transform.Find("MusicIcon").GetComponent<Image>();
            _audioIcon = _toggleAudioButton.transform.Find("AudioIcon").GetComponent<Image>();

            _tutorialOffIcon = null; // Missing icon

            LoadSettingPreferences();
            Debug.Log("tutorial: " + _tutorialEnabled);
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

            ChangePlayButtonIcon();

            if (_tutorialEnabled)
            {
                _tutorialIcon.sprite = _tutorialOnIcon;
                _tutorialIcon.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                _tutorialIcon.sprite = _tutorialOffIcon;
                _tutorialIcon.color = new Color(1f, 1f, 1f, 0f);
            }

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

            if (_currentLanguage.Equals("english"))
            {
                _languageIcon.sprite = _englishIcon;
            }
            else
            {
                _languageIcon.sprite = _finnishIcon;
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
            _tutorialEnabled = PlayerPrefs.GetInt(_tutorialEnabledKey, 1) == 1;
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

        public void ChangeLanguage()
        {
            if (_currentLanguage.Equals("english"))
            {
                LocalizationSettings.SelectedLocale = _finnishLocale;
                _languageIcon.sprite = _finnishIcon;
                _currentLanguage = "finnish";
            }
            else if (_currentLanguage.Equals("finnish"))
            {
                LocalizationSettings.SelectedLocale = _englishLocale;
                _languageIcon.sprite = _englishIcon;
                _currentLanguage = "english";
            }
            ChangePlayButtonIcon();
            PlayerPrefs.SetString(_currentLanguageKey, _currentLanguage);
            PlayerPrefs.Save();
        }

        public void ToggleTutorial()
        {
            if (_tutorialEnabled)
            {
                _tutorialIcon.sprite = _tutorialOffIcon;
                _tutorialIcon.color = new Color(1f, 1f, 1f, 0f);
                _tutorialEnabled = false;
                // TODO: Turn off tutorial
            }
            else
            {
                _tutorialIcon.sprite = _tutorialOnIcon;
                _tutorialIcon.color = new Color(1f, 1f, 1f, 1f);
                _tutorialEnabled = true;
                // TODO: Implement tutorial and turn it on
            }
            PlayerPrefs.SetInt(_tutorialEnabledKey, _tutorialEnabled ? 1 : 0);
            PlayerPrefs.Save();
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

        private void WriteHighScores()
        {
            if (HighScoreManager.Instance.HighScores.Count > 0)
            {
                _highScoreText1.text = "1. " + HighScoreManager.Instance.HighScores[0];
            }
            else
            {
                _highScoreText1.text = "1.";
            }

            if (HighScoreManager.Instance.HighScores.Count > 1)
            {
                _highScoreText2.text = "2. " + HighScoreManager.Instance.HighScores[1];
            }
            else
            {
                _highScoreText2.text = "2.";
            }

            if (HighScoreManager.Instance.HighScores.Count > 2)
            {
                _highScoreText3.text = "3. " + HighScoreManager.Instance.HighScores[2];
            }
            else
            {
                _highScoreText3.text = "3.";
            }
        }
    }
}

