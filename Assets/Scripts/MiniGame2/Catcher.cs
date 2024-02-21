using UnityEngine;

namespace MiniGame2
{
    public class Catcher : MonoBehaviour
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
                _isScored = true;
                _gameStatsManager.UpdateStats(_isScored);
            }

            else if (other.CompareTag("UndesiredProduct"))
            {
                _isScored = false;
                _gameStatsManager.UpdateStats(_isScored);
            }
        }
    }
}


