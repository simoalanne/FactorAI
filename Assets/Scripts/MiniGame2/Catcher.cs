using UnityEngine;

namespace Minigame2
{
    public class Catcher : MonoBehaviour
    {
        private GameStatsManager _gameStatsManager;
        private bool _isScored;

        private void Awake()
        {
            _gameStatsManager = FindObjectOfType<GameStatsManager>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("DesiredProduct"))
            {
                _isScored = true;
                _gameStatsManager.UpdateStats(_isScored);
                Destroy(collision.gameObject);
            }

            else if (collision.gameObject.CompareTag("UndesiredProduct"))
            {
                _isScored = false;
                _gameStatsManager.UpdateStats(_isScored);
                Destroy(collision.gameObject);
            }
        }
    }
}
