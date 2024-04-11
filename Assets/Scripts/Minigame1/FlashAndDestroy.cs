using UnityEngine;

namespace Minigame1
{

    public class FlashAndDestroy : MonoBehaviour
    {
        [SerializeField] private float _flashrate = 0.5f;
        [SerializeField] private float _flashingtime = 3f;

        private SpriteRenderer _spriteRenderer;
        private float _timePassed = 0;

        void Start()
        {
            _timePassed = 0;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.enabled = false;
            InvokeRepeating(nameof(Flash), 0, _flashrate);
        }

        void Flash()
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            _timePassed += _flashrate;
            if (_timePassed >= _flashingtime)
            {
                CancelInvoke(nameof(Flash));
                Destroy(gameObject);
            }
        }
    }
}
