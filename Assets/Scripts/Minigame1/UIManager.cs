using UnityEngine;
using TMPro;

namespace Minigame1
{
    public class UIManager : MonoBehaviour

    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _completedProdcutsText;

        private GameStatsManager _gameStatsManager;

        void Start()
        {
            _gameStatsManager = GetComponent<GameStatsManager>();
        }

        void Update()
        {
            _timerText.text = _gameStatsManager.MinigameTime;
            _scoreText.text = _gameStatsManager.Score.ToString();
            _completedProdcutsText.text = _gameStatsManager.CompletedProducts + "/" + _gameStatsManager.MinCompletedProducts;
        }
    }
}