using TMPro;
using UnityEngine;
using Global;

namespace Factory
{
    public class GameEnd : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private float _scoreCountDownRate = 0.0005f;
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
            _scoreCountDown += 500f;
            _scoreText.text = _scoreCountDown.ToString();
        }
    }
}

