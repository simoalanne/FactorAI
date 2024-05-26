using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

namespace Global
{
    public class MinigameFailedMenu : MonoBehaviour
    {
        [SerializeField] private int _timeToClick = 5;
        public int TimeToClick => _timeToClick;
        private TextMeshProUGUI _timerText;

        private void Awake()
        {
            _timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
            FindObjectOfType<CircleTimerAnimation>().AnimationStarted = true;
            InvokeRepeating(nameof(UpdateTimerText), 0, 1f);
        }

        public void RestartMinigame()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        private void UpdateTimerText()
        {
            _timerText.text = _timeToClick.ToString();
            _timeToClick -= 1;

            if (_timeToClick == -1)
            {
                CancelInvoke(nameof(UpdateTimerText));
                SceneManager.LoadSceneAsync("Factory");
            }
            else
            {
                StartCoroutine(PulseText());
            }
        }

        private IEnumerator PulseText()
        {
            float cooldownTime = 0.1f; // magic number for cooldown time
            float originalFontSize = _timerText.fontSize;
            float targetFontSize = originalFontSize + 50; // Increase font size by magic number 33

            // Instantly scale up
            _timerText.fontSize = targetFontSize;

            // Wait for cooldown
            yield return new WaitForSeconds(cooldownTime);

            // Instantly reset font size
            _timerText.fontSize = originalFontSize;
        }
    }
}