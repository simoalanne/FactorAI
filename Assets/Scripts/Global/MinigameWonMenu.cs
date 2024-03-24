using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class MinigameWonMenu : MonoBehaviour
    {
        public void BackToFactory()
        {
            SceneManager.LoadSceneAsync("Factory");
        }
    }
}
