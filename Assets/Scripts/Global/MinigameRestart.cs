
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Global
{
    public class MinigameRestart : MonoBehaviour
    {
        [SerializeField] private int _timeToClick = 5;

        private TextMeshProUGUI _timerText;

        private void Awake()
        {
            _timerText = transform.Find("Timer").GetComponent<TextMeshProUGUI>();
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
        }
    }
}