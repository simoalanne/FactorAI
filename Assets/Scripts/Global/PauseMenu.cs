using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Global
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoadFromQuit = "Factory";
        [SerializeField] private Button _tutorialButton;
        private GameObject _pauseButton;
        private GameObject _gamePausedMenu;
        private GameObject _confirmRestartMenu;
        private bool _gamePaused = false;
        public bool GamePaused => _gamePaused;
        private RaycastManager _raycastManager;
        private bool _rayCastEnabled = true;




        void Awake()
        {
            _pauseButton = transform.Find("PauseButton").gameObject;
            _gamePausedMenu = transform.Find("GamePausedMenu").gameObject;
            Transform foundTransform = transform.Find("ConfirmRestartMenu");
            _confirmRestartMenu = foundTransform != null ? foundTransform.gameObject : null;
            _pauseButton.SetActive(true);
            _gamePausedMenu.SetActive(false);
            _raycastManager = GetComponent<RaycastManager>();
        }

        public void PauseGame()
        {
            if (!_gamePaused)
            {
                Time.timeScale = 0;
                _raycastManager.DisableOtherCanvasesRaycasting();
                _gamePaused = true;
                _gamePausedMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                _raycastManager.EnableOtherCanvasesRaycasting();
                _gamePaused = false;
                _gamePausedMenu.SetActive(false);
            }
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            _raycastManager.EnableOtherCanvasesRaycasting();
            _gamePausedMenu.SetActive(false);
            _gamePaused = false;
        }

        public void OpenTutorialDialog()
        {
            ResumeGame();
            FindObjectOfType<Tutorial.GameTutorial>().StartTutorial();
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
            SceneManager.LoadSceneAsync("Title");
        }
    }
}


