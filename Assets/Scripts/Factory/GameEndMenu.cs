using TMPro;
using UnityEngine;
using Global;

namespace Factory
{
    public class GameEndMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private float _scorePerInvoke = 100f;
        [SerializeField] private float _scoreCountDownRate = 0.05f;
        private float _scoreCountDown = 0f;

        private void Start()
        {
            InvokeRepeating(nameof(UpdateScore), 0f, _scoreCountDownRate);
        }

        public void BackToTitle()
        {
            GameManager.Instance.ResetSaveData();
        }

        private void UpdateScore()
        {
            if (_scoreCountDown >= GameManager.Instance.GameScore) return;
            _scoreCountDown += _scorePerInvoke;
            _scoreText.text = _scoreCountDown.ToString();
        }
    }
}

