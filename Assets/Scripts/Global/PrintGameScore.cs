using UnityEngine;
using TMPro;

namespace Global
{
    public class PrintGameScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI GameScoreDisplay;

        void Update()
        {
            if (GameManager.Instance != null)
            {
                GameScoreDisplay.text = GameManager.Instance.GameScore.ToString();
            }
        }
    }
}
