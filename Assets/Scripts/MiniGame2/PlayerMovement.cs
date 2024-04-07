using UnityEngine;
using GameInputs;
using Global;

namespace Minigame2
{
    public class PlayerMovement : MonoBehaviour
    {
        private Inputs _inputs = null;
        private bool _dragStarted = true;
        private Vector3 _startDragPosition;
        private Vector2 _dragOffset;
        private Vector2 _originalPosition;
        private Vector2 _newPosition;

        public Vector2 DragOffset => _dragOffset;

        private void Awake()
        {
            _inputs = new Inputs();

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

            // Calculate the camera's boundaries
            float camHeight = Camera.main.orthographicSize;
            float camWidth = camHeight * Camera.main.aspect;
            float minX = Camera.main.transform.position.x - camWidth;
            float maxX = Camera.main.transform.position.x + camWidth;

            // Check if the object is outside the camera's boundaries and reposition it
            if (transform.position.x < minX)
            {
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
                _dragStarted = true; // Reset dragStarted, don't know why but it works
            }
            else if (transform.position.x > maxX)
            {
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);
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
            // Only add the x-component of the drag offset to the x-component of the original position
            _newPosition = new Vector2(_originalPosition.x + _dragOffset.x, _originalPosition.y);
            if (FindObjectOfType<InitGame>().GameStarted && FindObjectOfType<PauseMenu>().GamePaused == false)
            {
            transform.position = new Vector2(_newPosition.x, transform.position.y);
            }
        }
    }
}