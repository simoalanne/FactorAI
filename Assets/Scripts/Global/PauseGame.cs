using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Global
{
    public class PauseGame : MonoBehaviour
    {
        private Button _pauseButton;
        private TextMeshProUGUI _pauseText;
        private bool _isPaused;

        void Start()
        {
            _pauseButton = GetComponentInChildren<Button>();
            _pauseButton.onClick.AddListener(Pause);

            _pauseText = transform.Find("PauseText").GetComponent<TextMeshProUGUI>();
        }

        void Pause()
        {
            if (_isPaused)
            {
                Time.timeScale = 1;
                _pauseText.enabled = false;
                _isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                _pauseText.enabled = true;
                _isPaused = true;
            }
        }
    }
}

