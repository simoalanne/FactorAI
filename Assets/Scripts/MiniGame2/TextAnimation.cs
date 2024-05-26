using UnityEngine;
using TMPro;

namespace Minigame2
{
    public class TextAnimation : MonoBehaviour
    {
        private TMP_Text _text;
        private float originalFontSize;
        [SerializeField] private float _pulseSpeed = 0.25f;
        [SerializeField] private float _pulseScale = 0.1f;

        void Start()
        {
            _text = GetComponent<TMP_Text>();
            originalFontSize = _text.fontSize;
        }

        void Update()
        {
            float scaleFactor = 1f - Mathf.PingPong(Time.time * _pulseSpeed, _pulseScale);
            _text.fontSize = originalFontSize * scaleFactor;
        }
    }
}