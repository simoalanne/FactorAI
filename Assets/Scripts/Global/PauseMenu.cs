using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoadFromQuit = "Factory";
        private GameObject _pauseButton;
        private GameObject _gamePausedMenu;
        private GameObject _confirmRestartMenu;


        void Awake()
        {
            _pauseButton = transform.Find("PauseButton").gameObject;
            _gamePausedMenu = transform.Find("GamePausedMenu").gameObject;
            Transform foundTransform = transform.Find("ConfirmRestartMenu");
            _confirmRestartMenu = foundTransform != null ? foundTransform.gameObject : null;
            _pauseButton.SetActive(true);
            _gamePausedMenu.SetActive(false);
        }

        public void PauseGame()
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                _gamePausedMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                _gamePausedMenu.SetActive(false);
            }
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            _gamePausedMenu.SetActive(false);
        }

        public void Quit()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(_sceneToLoadFromQuit);
        }

        public void OpenConfirmRestartMenu()
        {
            _gamePausedMenu.SetActive(false);
            _pauseButton.SetActive(false);
            _confirmRestartMenu.SetActive(true);
        }

        public void CancelRestart()
        {
            _pauseButton.SetActive(true);
            _gamePausedMenu.SetActive(true);
            _confirmRestartMenu.SetActive(false);
        }

        public void RestartGameSave()
        {
            _confirmRestartMenu.SetActive(false);
            Time.timeScale = 1;
            GameManager.Instance.ResetSaveData();
        }

    }
}


