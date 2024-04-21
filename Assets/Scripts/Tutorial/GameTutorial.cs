using System.Collections;
using Global;
using TMPro;
using UnityEngine;

namespace Tutorial
{
    public class GameTutorial : MonoBehaviour
    {
        [SerializeField] private GameObject _tutorialDialog;
        [SerializeField] private TMP_Text[] _tutorialParts; // Array of tutorial parts
        [SerializeField] private float _textSpeed = 0.1f;
        private int _counter = 0;
        private int _currentPart = 0; // Index of the current tutorial part
        private Coroutine _revealTextCoroutine; // Reference to the coroutine

        void Awake()
        {
            InitTutorial();
        }

        public void StartTutorial()
        {
            Time.timeScale = 0;
            GetComponent<RaycastManager>().DisableOtherCanvasesRaycasting();
            _tutorialDialog.SetActive(true);
            _tutorialParts[_currentPart].gameObject.SetActive(true); // Set the current part active
            _counter = 0; // Reset the counter
            _revealTextCoroutine = StartCoroutine(RevealText(_tutorialParts[_currentPart], _textSpeed));
        }

        private IEnumerator RevealText(TMP_Text textComponent, float speed)
        {
            while (_counter <= textComponent.text.Length)
            {
                textComponent.maxVisibleCharacters = _counter;
                _counter++;
                yield return new WaitForSecondsRealtime(speed);
            }
        }

        public void NextPart()
        {
            Debug.Log("Next part");
            if (_revealTextCoroutine != null)
            {
                StopCoroutine(_revealTextCoroutine); // Stop the coroutine if it's running
            }

            if (_currentPart < _tutorialParts.Length - 1)
            {
                _tutorialParts[_currentPart].gameObject.SetActive(false); // Set the current part inactive
                _currentPart++; // Move to the next tutorial part
                StartTutorial(); // Start the tutorial for the next part
            }
            else
            {
                _tutorialDialog.SetActive(false);
                GetComponent<RaycastManager>().EnableOtherCanvasesRaycasting();

                if (GameManager.Instance.FirstTimePlaying)
                {
                    GameManager.Instance.FirstTimePlaying = false; // Tutorial completed for the first time, set FirstTimePlaying to false
                    GameManager.Instance.SaveGameData(); // Save the game data
                    FindObjectOfType<Factory.UIManager>().EnablePlayButtonOrProductSelection(); // Start the game
                }
                InitTutorial();
                Time.timeScale = 1;
            }
        }

        private void InitTutorial()
        {
            foreach (var part in _tutorialParts)
            {
                part.maxVisibleCharacters = 0;
            }

            if (GameManager.Instance.FirstTimePlaying == false)
            {
                _currentPart = 2; // Skip the first two parts of the tutorial when it's not the first time playing
            }
            else
            {
                _currentPart = 0;
            }
        }
    }
}