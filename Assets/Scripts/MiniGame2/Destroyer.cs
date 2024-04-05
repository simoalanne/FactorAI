using UnityEngine;
using Global;

namespace Minigame2
{
    public class Destroyer : MonoBehaviour
    {
        private GameStatsManager _gameStatsManager;
        private bool _scored;

        private void Awake()
        {
            _gameStatsManager = FindObjectOfType<GameStatsManager>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("DesiredProduct"))
            {
                _gameStatsManager.UpdateStats(_scored, collision.transform.position);
                Destroy(collision.gameObject);
            }

            else if (collision.gameObject.CompareTag("UndesiredProduct"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
