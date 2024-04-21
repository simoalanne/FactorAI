using UnityEngine;
using TMPro;
using Global;
using UnityEngine.UI;

namespace Minigame2
{
    public class InitGame : MonoBehaviour
    {

        [SerializeField] private float _gameStartDelay = 3f;
        [SerializeField] private TMP_Text _tapText;
        [SerializeField] private TMP_Text _getReadyText;
        [SerializeField] private TMP_Text _countdownText;
        private bool _gameStarted;
        public bool GameStarted => _gameStarted;


        private float _gameStartCountdown;
        private PlayerMovement _playerMovement;
        private bool _tapped;

        private ManageSprites _manageSprites;

        void Awake()
        {
            _tapText.enabled = true;
            _getReadyText.enabled = false;
            _countdownText.enabled = false;
            _playerMovement = GetComponentInParent<PlayerMovement>();
            _playerMovement.enabled = false;
            _manageSprites = FindObjectOfType<ManageSprites>();
            _manageSprites.enabled = false;
        }

        void Update()
        {

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !_tapped)
            {
                _tapped = true;
                InvokeRepeating(nameof(StartGameCountdown), 0f, 1f);
            }
        }

        private void StartGameCountdown()
        {
            _tapText.enabled = false;
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
                _playerMovement.enabled = true;
            }
        }
    }
}



