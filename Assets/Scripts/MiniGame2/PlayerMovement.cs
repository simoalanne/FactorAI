using UnityEngine;
using GameInputs;

namespace Minigame2
{
    public class PlayerMovement : MonoBehaviour
    {
        private Inputs _inputs = null;
        private bool _dragStarted = true;
        private Vector3 _startDragPosition;
        private Vector2 _dragOffset;
        private Vector2 _originalPosition;
        private Rigidbody2D _rb;
        private Vector2 _newPosition;

        public Vector2 DragOffset => _dragOffset;

        private void Awake()
        {
            _inputs = new Inputs();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _inputs.MiniGame2.Enable();
        }

        void OnDisable()
        {
            _inputs.MiniGame2.Disable();
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                StartDragging();
            }
            else
            {
                _dragStarted = true;
            }
        }

        private void StartDragging()
        {
            // On first method call, set the start position and the original position
            if (_dragStarted)
            {
                _startDragPosition = Camera.main.ScreenToWorldPoint(_inputs.MiniGame2.Drag.ReadValue<Vector2>());
                _originalPosition = transform.position;
                _dragStarted = false;
            }

            // Calculate the offset between the start of the drag and the current position
            _dragOffset = Camera.main.ScreenToWorldPoint(_inputs.MiniGame2.Drag.ReadValue<Vector2>()) - _startDragPosition;
            _newPosition = _originalPosition + _dragOffset;
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_newPosition);
        }
    }
}