using UnityEngine;
using TMPro;

namespace Global
{
    public class PrintMainTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI MainTimerDisplay;

        void Update()
        {
            if (GameManager.Instance != null)
            {
                MainTimerDisplay.text = GameManager.Instance.GameTimer;
            }
        }
    }
}
