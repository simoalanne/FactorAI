
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Global
{
    public class MinigameRestart : MonoBehaviour
    {
        [SerializeField] private Canvas[] _canvasesToDisable;

        [SerializeField, Tooltip("Disable all the spawners and other objects that you don't want active during this script")]
        private GameObject[] _otherGameObjectsToDisable;

        [SerializeField] private int _timeToClick = 5;

        private TextMeshProUGUI _timerText;

        private void Awake()
        {
            foreach (Canvas canvas in _canvasesToDisable)
            {
                canvas.enabled = false;
            }

            foreach (GameObject gameObject in _otherGameObjectsToDisable)
            {
                gameObject.SetActive(false);
            }

            _timerText = transform.Find("Timer").GetComponent<TextMeshProUGUI>();

            InvokeRepeating(nameof(UpdateTimerText), 0, 1.25f);
        }

        public void RestartMinigame()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        private void UpdateTimerText()
        {
            _timerText.text = _timeToClick.ToString();
            _timeToClick -= 1;

            if (_timeToClick == 0)
            {
                CancelInvoke(nameof(UpdateTimerText));
                SceneManager.LoadSceneAsync("Factory");
            }
        }
    }
}