using UnityEngine;
using Audio;

namespace Minigame2
{
    public class Destroyer : MonoBehaviour
    {
        private GameStatsManager _gameStatsManager;
        private SoundEffectPlayer _soundEffectPlayer;

        private void Awake()
        {
            _gameStatsManager = FindObjectOfType<GameStatsManager>();
            _soundEffectPlayer = GetComponent<SoundEffectPlayer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_gameStatsManager.GameActive == false)
            {
                Destroy(collision.gameObject);
                return; // If the game is not active, no need to do anything else other than destroying the object
            }

            if (collision.gameObject.CompareTag("DesiredProduct"))
            {
                _gameStatsManager.UpdateStats(false, collision.transform.position);
                Destroy(collision.gameObject);
                _soundEffectPlayer.PlaySoundEffect(0);
            }

            else if (collision.gameObject.CompareTag("UndesiredProduct"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
