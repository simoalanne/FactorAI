using UnityEngine;
using UnityEngine.UI;

namespace Factory
{
    public class LineAnimation : MonoBehaviour
    {
        [SerializeField] private RawImage _lineImage1;
        [SerializeField] private RawImage _lineImage2;
        [SerializeField] private float _rectSpeed = 0.1f;

        void Update()
        {
            _lineImage1.uvRect = new Rect(_lineImage1.uvRect.position + new Vector2(- _rectSpeed, 0) * Time.deltaTime, _lineImage1.uvRect.size);
            _lineImage2.uvRect = new Rect(_lineImage2.uvRect.position + new Vector2( _rectSpeed, 0) * Time.deltaTime, _lineImage2.uvRect.size); 
        }
    }
}
