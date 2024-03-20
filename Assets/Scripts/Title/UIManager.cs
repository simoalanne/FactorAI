using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Title
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _titlePanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private string _sceneToLoad;
        [SerializeField] private Button _toggleMusicButton;
        [SerializeField] private Sprite _musicOnIcon;
        [SerializeField] private Sprite _musicOffIcon;
        [SerializeField] private Button _changeLanguageButton;
        [SerializeField] private Sprite _englishFlag;
        [SerializeField] private Sprite _finnishFlag;

        private bool _musicEnabled = true;
        private string _currentLanguage = "English";

        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(_sceneToLoad);
        }

        public void OpenSettings()
        {
            _titlePanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        public void CloseSettings()
        {
            _settingsPanel.SetActive(false);
            _titlePanel.SetActive(true);
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

        public void ChangeLanguage()
        {
            if (_currentLanguage == "English")
            {
                _changeLanguageButton.image.sprite = _finnishFlag;
                _currentLanguage = "Finnish";
            }
            else if (_currentLanguage == "Finnish")
            {
                _changeLanguageButton.image.sprite = _englishFlag;
                _currentLanguage = "English";
            }
        }
    }
}

