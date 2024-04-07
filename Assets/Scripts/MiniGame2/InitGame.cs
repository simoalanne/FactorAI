using UnityEngine;
using TMPro;
using Global;
using UnityEngine.UI;

namespace Minigame2
{
    public class InitGame : MonoBehaviour
    {
        [SerializeField, Tooltip("Amount player has to swipe before the game can start")]
        private float _swipeThreshold = 1f;

        [SerializeField] private float _gameStartDelay = 3f;
        [SerializeField] private TMP_Text _swipeText;
        [SerializeField] private TMP_Text _getReadyText;
        [SerializeField] private TMP_Text _countdownText;
        private bool _gameStarted;
        public bool GameStarted => _gameStarted;


        private float _gameStartCountdown;
        private PlayerMovement _playerMovement;
        private bool _swiped;

        private ManageSprites _manageSprites;

        void Awake()
        {
            _swipeText.enabled = true;
            _getReadyText.enabled = false;
            _countdownText.enabled = false;
            _playerMovement = GetComponentInParent<PlayerMovement>();
            _manageSprites = FindObjectOfType<ManageSprites>();
            _manageSprites.enabled = false;
            FindObjectOfType<PauseMenu>().GetComponentInChildren<Button>().interactable = false;
        }

        void Update()
        {
            if (_playerMovement.DragOffset.x >= _swipeThreshold && !_swiped)
            {
                _swiped = true;
                InvokeRepeating(nameof(StartGameCountdown), 1, 1f);
            }
        }

        private void StartGameCountdown()
        {
            _swipeText.enabled = false;
            _getReadyText.enabled = true;
            _countdownText.enabled = true;
            _countdownText.text = (_gameStartDelay - _gameStartCountdown).ToString();
            _gameStartCountdown += 1f;
            if (_gameStartCountdown > _gameStartDelay + 1f)
            {
                Time.timeScale = 1f;
                CancelInvoke(nameof(StartGameCountdown));
                _getReadyText.enabled = false;
                _countdownText.enabled = false;
                _manageSprites.enabled = true;
                _gameStarted = true;
                FindObjectOfType<PauseMenu>().GetComponentInChildren<Button>().interactable = true;
            }
        }
    }
}



