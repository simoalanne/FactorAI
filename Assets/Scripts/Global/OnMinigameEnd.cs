using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Global
{
    public class OnMinigameEnd : MonoBehaviour
    {
        // This one can be found on UI prefabs folder
        [SerializeField] private GameObject _minigameRestartMenu;

        public void OnGameLost()
        {
            _minigameRestartMenu.SetActive(true);
        }

        public void OnGameWon(float minigameScore)
        {
            if (GameManager.Instance == null) return;
            GameManager.Instance.AddToGameScore(minigameScore);
            GameManager.Instance.AddAiSkipProgress(minigameScore);
            GameManager.Instance.ChangeActiveMiniGame();
            SceneManager.LoadScene("Factory");
        }
    }
}












