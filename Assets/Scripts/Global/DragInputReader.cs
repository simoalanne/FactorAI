using UnityEngine;
using GameInputs;

namespace InputReading
{
    public class DragInputReader : MonoBehaviour
    {
        public Inputs _inputs = null;
        private Vector2 _dragValue;
        public Vector2 DragValue => _dragValue;
        

        private void Awake()
        {
            _inputs = new Inputs();
        }

        private void OnEnable()
        {
            _inputs.Minigames.Enable();
        }

        private void OnDisable()
        {
            _inputs.Minigames.Disable();
        }

        private void Update()
        {
            _dragValue = _inputs.Minigames.Drag.ReadValue<Vector2>();
        }
    }
}