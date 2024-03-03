using UnityEngine;
using GameInputs;

namespace MiniGame2
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
            _inputs.MiniGame2.Enable();
        }

        void OnDisable()
        {
            _inputs.MiniGame2.Disable();
        }

        private void Update()
        {
            if (_inputs.MiniGame2.MoveLeft.IsPressed())
            {
                _movementInput = Vector2.left;
            }
            
            else if (_inputs.MiniGame2.MoveRight.IsPressed())
            {
                _movementInput = Vector2.right;
            }

            else
            {
                _movementInput = Vector2.zero;
            }
            
        }
    }
}
