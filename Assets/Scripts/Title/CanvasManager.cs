using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Title
{
    public class OnNewGameClick : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoad;
        
        private Button _newGameButton;    

        void Start()
        {
            _newGameButton = GetComponent<Button>();
            _newGameButton.onClick.AddListener(StartNewGame);
        }

        void StartNewGame()
        {
            SceneManager.LoadSceneAsync(_sceneToLoad);
            // TODO: Create a seperate sceneloader script for fancier loading animations
            // TODO: Reset the game save file when starting new game.
        }
    }
}

