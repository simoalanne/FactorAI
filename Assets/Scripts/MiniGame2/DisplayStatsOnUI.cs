using UnityEngine;
using TMPro;

namespace MiniGame2
{
    public class DisplayStatsOnUI : MonoBehaviour
    {
        private GameStatsManager gameStatsManager;
        [SerializeField] private TextMeshProUGUI timerDisplay;
        [SerializeField] private TextMeshProUGUI scoreDisplay;
        [SerializeField] private TextMeshProUGUI failsDisplay;

        void Start()
        {
            gameStatsManager = GetComponent<GameStatsManager>();
            timerDisplay.alignment = TextAlignmentOptions.Right;
            scoreDisplay.alignment = TextAlignmentOptions.Right;
            failsDisplay.alignment = TextAlignmentOptions.Right;
            timerDisplay.color = Color.blue;
            scoreDisplay.color = Color.green;
            failsDisplay.color = Color.red;

        }

        void Update()
        {
            timerDisplay.text = "Time: " + gameStatsManager.PrintTime;
            scoreDisplay.text = "score: " + gameStatsManager.Score.ToString();
            failsDisplay.text = "fails: " + gameStatsManager.Fails.ToString();
        }
    }
}
