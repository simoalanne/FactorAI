
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Global;

namespace Factory
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private string _minigameToLoad;
        [SerializeField] private float _aiSkipScoreAmount;

        private Button _playButton;
        private Button _aiSkipButton;

        void Start()
        {
            _playButton = transform.Find("PlayButton").GetComponent<Button>();
            _playButton.onClick.AddListener(LoadMinigame);

            _aiSkipButton = transform.Find("AISkipButton").GetComponent<Button>();
            
            if (GameManager.Instance.IsAiSkipReady)
            {
                _aiSkipButton.interactable = true;
                _aiSkipButton.onClick.AddListener(UseAiSkip);
            }
        }

        void LoadMinigame()
        {
            SceneManager.LoadSceneAsync(_minigameToLoad);
        }

        void UseAiSkip()
        {
            _aiSkipButton.interactable = false;
            GameManager.Instance.AddToGameScore(_aiSkipScoreAmount);
            GameManager.Instance.IsAiSkipReady = false;
        }
    }
}
