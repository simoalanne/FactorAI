using UnityEngine;
using TMPro;

namespace Minigame2
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text failsText;
        [SerializeField] private TMP_Text _collectedText;

        private GameStatsManager _gameStatsManager;

        private string RepeatedString(string text, int times)
        {
            string result = "";
            for (int i = 0; i < times; i++)
            {
                result += text;
            }
            return result;
        }

        void Start()
        {
            _gameStatsManager = GetComponent<GameStatsManager>();
        }

        void Update()
        {
            scoreText.text = "SCORE: \n " + _gameStatsManager.Score;
            failsText.text = RepeatedString("X ", _gameStatsManager.Fails);
            _collectedText.text = "PICKED: \n" + _gameStatsManager.Collected + " / " + _gameStatsManager.HowManyForWin;
        }
    }
}
