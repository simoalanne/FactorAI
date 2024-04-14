using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Global
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoadFromQuit = "Factory";
        [SerializeField] private Canvas[] _canvasesToDisableRaycasting;
        private GameObject _pauseButton;
        private GameObject _gamePausedMenu;
        private GameObject _confirmRestartMenu;
        private bool _gamePaused = false;
        public bool GamePaused => _gamePaused;


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
            if (!_gamePaused)
            {
                Time.timeScale = 0;
                _gamePaused = true;
                DisableOrEnableRaycastingForOtherCanvases();
                _gamePausedMenu.SetActive(true);
                MusicPlayer.Instance.PauseMusic();
            }
            else
            {
                Time.timeScale = 1;
                DisableOrEnableRaycastingForOtherCanvases();
                _gamePausedMenu.SetActive(false);
                _gamePaused = false;
                MusicPlayer.Instance.UnpauseMusic();
            }
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            DisableOrEnableRaycastingForOtherCanvases();
            _gamePausedMenu.SetActive(false);
            _gamePaused = false;
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

        private void DisableOrEnableRaycastingForOtherCanvases()
        {
            if (_canvasesToDisableRaycasting.Length < 0) return;

            foreach (Canvas canvas in _canvasesToDisableRaycasting)
            {
                if (canvas.GetComponent<GraphicRaycaster>().enabled == true)
                {
                    canvas.GetComponent<GraphicRaycaster>().enabled = false;
                }
                else if (canvas.GetComponent<GraphicRaycaster>().enabled == false)
                {
                    canvas.GetComponent<GraphicRaycaster>().enabled = true;
                }

                Debug.Log("Raycasting enabled:" + canvas.GetComponent<GraphicRaycaster>().enabled);
            }
        }
    }
}


