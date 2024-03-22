using UnityEngine;
using TMPro;

namespace Minigame2
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text failsText;
        [SerializeField] private TMP_Text _collectedText;

        private GameStatsManager _gameStatsManager;

        void Start()
        {
            _gameStatsManager = GetComponent<GameStatsManager>();
        }
  
        void Update()
        {
            timerText.text = "TIME: \n" + _gameStatsManager.MinigameTime;
            scoreText.text = "SCORE: \n " + _gameStatsManager.Score;
            failsText.text = "FAILS: \n" + _gameStatsManager.Fails + " / " + _gameStatsManager.MaxFails;
            _collectedText.text = "PICKED: \n" + _gameStatsManager.Collected + " / " + _gameStatsManager.HowManyForWin;

        }
    }
}
