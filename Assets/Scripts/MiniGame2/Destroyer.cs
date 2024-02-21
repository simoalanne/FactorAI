using UnityEngine;

namespace MiniGame2
{
    public class Destroyer : MonoBehaviour
    {
        private GameStatsManager _gameStatsManager;
        private bool _isScored;

        private void Awake()
        {
            _gameStatsManager = FindObjectOfType<GameStatsManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DesiredProduct"))
            {
                _isScored = false;
                _gameStatsManager.UpdateStats(_isScored);
            }
        }
    }
}
