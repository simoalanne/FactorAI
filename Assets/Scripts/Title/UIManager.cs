using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SocialPlatforms;

namespace Title
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _titleMenu;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private string _sceneToLoad;
        [SerializeField] private Button _toggleMusicButton;
        [SerializeField] private Sprite _musicOnIcon;
        [SerializeField] private Sprite _musicOffIcon;
        [SerializeField] private Button _changeLanguageButton;
        [SerializeField] private Locale _englishLocale;
        [SerializeField] private Locale _finnishLocale;
     
        private bool _musicEnabled = true;
    

        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(_sceneToLoad);
        }

        public void OpenSettings()
        {
            _titleMenu.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        public void CloseSettings()
        {
            _settingsPanel.SetActive(false);
            _titleMenu.SetActive(true);
        }

        public void ToggleMusic()
        {
            if (_musicEnabled)
            {

                _toggleMusicButton.image.sprite = _musicOffIcon;
                _musicEnabled = false;
                // TODO: Turn off music
            }
            else
            {
                _toggleMusicButton.image.sprite = _musicOnIcon;
                _musicEnabled = true;
                // TODO: Turn on music
            }
        }

        public void ChangeToEnglish()
        {
            if (_englishLocale != null && LocalizationSettings.SelectedLocale != _englishLocale)
            {
                Debug.Log("Changing to English");
                LocalizationSettings.SelectedLocale = _englishLocale;
            }
        }

        public void ChangeToFinnish()
        {
            if (_finnishLocale != null && LocalizationSettings.SelectedLocale != _finnishLocale)
            {
                Debug.Log("Changing to Finnish");
                LocalizationSettings.SelectedLocale = _finnishLocale;
            }
        }
    }
}

