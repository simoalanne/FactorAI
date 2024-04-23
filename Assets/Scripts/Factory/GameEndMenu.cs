using TMPro;
using UnityEngine;
using Global;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Factory
{
    public class GameEndMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text _newHighScoreText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private float _scorePerInvoke = 10000f;
        [SerializeField] private float _scoreCountDownRate = 0.005f;
        [SerializeField] private Button _saveAndExitButton;
        [SerializeField] private Button _exitWithoutSavingButton;
        [SerializeField] private GameObject[] _canvasesToDisable;
        [SerializeField] private ParticleSystem _confetti;
        [SerializeField] private GameObject _inputFieldBackground; // The background of the input field, this will also disable the input field if necessary
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private int _maxNameLength = 8;
        [SerializeField] private int _minNameLength = 1;
        [SerializeField] private TMP_Text _inputFieldText;
        private string _originalInputFieldText;
        private float _scoreCountDown = 0f;

        private void Awake()
        {
            foreach (GameObject canvas in _canvasesToDisable)
            {
                canvas.SetActive(false);
            }

            // Set up the input validation
            _inputField.onValidateInput += (string text, int charIndex, char addedChar) =>
            {
                if (text.Length >= _maxNameLength)
                {
                    return '\0'; // Reject the input if the text length is greater than the max name length
                }
                else if (char.IsLetter(addedChar))
                {
                    return addedChar; // Accept the input if it's a letter or a digit
                }
                else
                {
                    return '\0'; // Reject the input if it's not a letter or a digit
                }
            };

            // Disable all the buttons and input field until the score has finished counting down
            _inputFieldBackground.SetActive(false);
            _saveAndExitButton.gameObject.SetActive(false);
            _exitWithoutSavingButton.gameObject.SetActive(false);
            if (GameManager.Instance.GameScore <= 0)
            {
                Debug.LogWarning("Game score is 0, resetting save data");
                _scoreText.text = "0";
                GameManager.Instance.ResetSaveData();
                _exitWithoutSavingButton.gameObject.SetActive(true);
                _exitWithoutSavingButton.transform.Find("ExitText").GetComponent<TMP_Text>().enabled = true;
                return;
            }

            HighScoreManager.Instance.CheckForHighScore(GameManager.Instance.GameScore);
            GameManager.Instance.ResetSaveData(); // Reset save data here so if user quits the game and comes back, the game stats will be reset and they won't load straight into the game end menu.

            InvokeRepeating(nameof(UpdateScore), 0f, _scoreCountDownRate);
        }

        public void BackToTitle()
        {
            SceneManager.LoadSceneAsync("Title");
        }

        private void UpdateScore()
        {
            if (_scoreCountDown < GameManager.Instance.GameScore)
            {
                _scoreCountDown += _scorePerInvoke;
                _scoreText.text = _scoreCountDown.ToString();
            }
            else
            {
                if (HighScoreManager.Instance.NewHighScore)
                {
                    _newHighScoreText.enabled = true; // Enable the new high score text if a new high score was achieved
                    _confetti.Play(); // Play the confetti particle system if a new high score was achieved
                    _inputFieldBackground.SetActive(true);
                    _saveAndExitButton.gameObject.SetActive(true);
                    _saveAndExitButton.interactable = false;
                    _exitWithoutSavingButton.gameObject.SetActive(true);
                    _exitWithoutSavingButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(275, -250);
                    _exitWithoutSavingButton.transform.Find("ExitWithoutSavingText").GetComponent<TMP_Text>().enabled = true;
                }
                else
                {
                    _exitWithoutSavingButton.gameObject.SetActive(true);
                    _exitWithoutSavingButton.transform.Find("ExitText").GetComponent<TMP_Text>().enabled = true;
                }

                CancelInvoke(nameof(UpdateScore));
            }
        }

        public void OnValueChangedInputField()
        {
            if (_inputField.text.Length >= _minNameLength)
            {
                _saveAndExitButton.interactable = true;
            }
            else
            {
                _saveAndExitButton.interactable = false;
            }
        }

        public void OnSelectInputField()
        {
            Debug.Log("Input field selected");
            Debug.Log(_inputField.text);
            Debug.Log(_originalInputFieldText);
            if (_inputField.text == _originalInputFieldText) // If the input field text is the same as the original text, clear the input field
            {
                Debug.Log("Clearing input field");
                _inputField.text = "";
            }
        }

        public void SaveAndExit()
        {
            HighScoreManager.Instance.AddHighScore(_inputField.text, _scoreCountDown);
            BackToTitle();
        }

        public void ExitWithoutSaving()
        {
            BackToTitle();
        }
    }
}