using UnityEngine;
using TMPro;

namespace Global
{
    public class PrintGameScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI GameScoreDisplay;

        void Update()
        {
            GameScoreDisplay.text = GameManager.Instance.GameScore.ToString();
        }
    }
}
