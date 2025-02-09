using UnityEngine;

namespace Global
{
    public class OnMinigameEnd : MonoBehaviour
    {
        [SerializeField] private GameObject _minigameFailedMenu;
        [SerializeField] private GameObject _minigameWonMenu;
        [SerializeField] private Canvas[] _canvasesToDisable;
        [SerializeField] private GameObject[] _gameobjectsToDisable;


        private void Awake()
        {
            _minigameFailedMenu.SetActive(false);
            _minigameWonMenu.SetActive(false);
        }

        public void OnGameLost()
        {
            InitMenu();
            _minigameFailedMenu.SetActive(true);
        }

        public void OnGameWon(float minigameScore)
        {
            InitMenu();
            if (GameManager.Instance == null) return;
            GameManager.Instance.AddToGameScore(minigameScore);
            GameManager.Instance.AddAiSkipProgress(minigameScore);
            GameManager.Instance.ChangeActiveMiniGame();
            _minigameWonMenu.SetActive(true);
            FindObjectOfType<MinigameWonMenu>().DisplayScore(minigameScore);
        }

        private void InitMenu()
        {
            if (_canvasesToDisable != null)
            {
                foreach (Canvas canvas in _canvasesToDisable)
                {
                    canvas.enabled = false;
                }
            }

            if (_gameobjectsToDisable != null)
            {
                foreach (GameObject gameObject in _gameobjectsToDisable)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
