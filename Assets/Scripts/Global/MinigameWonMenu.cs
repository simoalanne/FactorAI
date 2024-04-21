using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Global
{
    public class MinigameWonMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text _valueText;
        public void BackToFactory()
        {
            SceneManager.LoadSceneAsync("Factory");
        }

        public void DisplayScore(float minigameScore)
        {
            _valueText.text = minigameScore.ToString();
        }
    }
}
