using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class MinigameWonMenu : MonoBehaviour
    {
        public void BackToFactory()
        {
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync("Factory");
        }
    }
}
