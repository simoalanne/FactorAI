using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    public class CircleTimerAnimation : MonoBehaviour
    {
        private float _totalTime;
        private float _timeLeft;
        private Image _timerImage;
        private bool _animationStarted = false;

        public bool AnimationStarted
        {
            get => _animationStarted;
            set => _animationStarted = value;
        }

        void Start()
        {
            _timerImage = GetComponent<Image>();
            _totalTime = FindObjectOfType<MinigameFailedMenu>().TimeToClick;
            _timeLeft = _totalTime;
        }

        void Update()
        {
            if (_animationStarted)
            {
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.deltaTime;
                    _timerImage.fillAmount = _timeLeft / _totalTime;
                }
                else
                {
                    _animationStarted = false;
                    _timeLeft = _totalTime;
                }
            }

        }
    }
}