
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Minigame2
{
    public class MinigameTutorial : MonoBehaviour
    {
        [SerializeField] private Sprite _productImage;
        [SerializeField] private Image _tutorialCanvas;
        [SerializeField] private Button _openTutorialButton;
        [SerializeField] private TMP_Text _goalText;

        void Start()
        {
            _goalText.text = FindObjectOfType<GameStatsManager>().HowManyForWin.ToString();

            if (_productImage != null)
            {
                transform.Find("ProductImage").GetComponent<Image>().sprite = _productImage;
            }
        }

        public void OpenTutorial()
        {
            Time.timeScale = 0;
            _openTutorialButton.gameObject.SetActive(false);
            _tutorialCanvas.gameObject.SetActive(true);
        }

        public void CloseTutorial()
        {
            _tutorialCanvas.gameObject.SetActive(false);
            _openTutorialButton.gameObject.SetActive(true);
            StartCoroutine(ResumeGame());
        }

        private IEnumerator ResumeGame()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            Time.timeScale = 1;
        }
    }
}
