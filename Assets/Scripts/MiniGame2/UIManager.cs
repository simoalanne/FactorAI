using UnityEngine;
using TMPro;
using System.Linq;

namespace Minigame2
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text failsText;
        [SerializeField] private TMP_Text _collectedText;

        private GameStatsManager _gameStatsManager;

        void Start()
        {
            _gameStatsManager = GetComponent<GameStatsManager>();
            failsText.text = GenerateFailText(_gameStatsManager.MaxFails, 0);
        }

        void Update()
        {
            scoreText.text = _gameStatsManager.Score.ToString();
            failsText.text = GenerateFailText(_gameStatsManager.MaxFails, _gameStatsManager.Fails);
            _collectedText.text = _gameStatsManager.Collected + "/" + _gameStatsManager.HowManyForWin;
        }

        private string GenerateFailText(int maxFails, int fails)
        {
            string gray = "<color=#80808050>"; // rgba(128, 128, 128, 0.5)
            string red = "<color=#FF0000FF>"; // rgba(255, 0, 0, 1)
            string endColor = "</color>";
            string redXs = string.Concat(Enumerable.Repeat(red + "X" + endColor + " ", fails));
            string grayXs = string.Concat(Enumerable.Repeat(gray + "X" + endColor + " ", maxFails - fails)); 
            return redXs + grayXs; 
        }
    }
}