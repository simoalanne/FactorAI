using UnityEngine;
using TMPro;

namespace MiniGame2
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI failsText;
        [SerializeField] private TextMeshProUGUI _collectedText;

        private GameStatsManager _gameStatsManager;

        void Start()
        {
            _gameStatsManager = GetComponent<GameStatsManager>();
        }
  
        void Update()
        {
            timerText.text = "TIME: \n" + _gameStatsManager.PrintTime;
            scoreText.text = "SCORE: \n " + _gameStatsManager.Score;
            failsText.text = "FAILS: \n" + _gameStatsManager.Fails + " / " + _gameStatsManager.MaxFails;
            _collectedText.text = "PICKED: \n" + _gameStatsManager.Collected + " / " + _gameStatsManager.HowManyForWin;

        }
    }
}
