using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame1
{
    public class InputReader : MonoBehaviour
    {
        private Inputs _inputs = null;
        private Vector2 _movementInput = Vector2.zero;

        public Vector2 Movement
        {
            get { return _movementInput; }
        }

        private void Awake()
        {
            _inputs = new Inputs();
        }

        private void OnEnable()
        {
            _inputs.Game.Enable();
        }

        void OnDisable()
        {
            _inputs.Game.Disable();
        }

        private void Update()
        {
            _movementInput = _inputs.Game.Move.ReadValue<Vector2>();
        }
    }
}
