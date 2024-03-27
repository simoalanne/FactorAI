using UnityEngine;
using TMPro;

namespace Minigame2
{
    public class InitGame : MonoBehaviour
    {
        [SerializeField, Tooltip("Amount player has to swipe before the game can start")]
        private float _swipeThreshold = 1f;

        [SerializeField] private float _gameStartDelay = 3f;
        [SerializeField] private TMP_Text _countdownText;

        private float _gameStartCountdown;
        private PlayerMovement _playerMovement;
        private bool _swiped;

        private ManageSprites _manageSprites;

        void Awake()
        {
            _playerMovement = GetComponentInParent<PlayerMovement>();
            _manageSprites = FindObjectOfType<ManageSprites>();
            _manageSprites.enabled = false;
        }

        void Update()
        {
            if (_playerMovement.DragOffset.x >= _swipeThreshold && !_swiped)
            {
                _swiped = true;
                InvokeRepeating(nameof(StartGameCountdown), 0f, 1f);
            }
        }

        private void StartGameCountdown()
        {
            _countdownText.text = "GET READY IN:\n" + (_gameStartDelay - _gameStartCountdown);
            _gameStartCountdown += 1f;
            if (_gameStartCountdown > _gameStartDelay + 1f)
            {
                Time.timeScale = 1f;
                CancelInvoke(nameof(StartGameCountdown));
                _countdownText.enabled = false;
                _manageSprites.enabled = true;
                Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
            }
        }
    }
}



