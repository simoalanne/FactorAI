using UnityEngine;
using GameInputs;

namespace Menu
{
    public class InputReader : MonoBehaviour
    {
        private Inputs _inputs = null;
        private bool _interactInput = false;

        public bool InteractInput => _interactInput;


        private void Awake()
        {
            _inputs = new Inputs();
        }

        private void OnEnable()
        {
            _inputs.Menu.Enable();
        }

        void OnDisable()
        {
            _inputs.Menu.Disable();
        }

        private void Update()
        {
            _interactInput = _inputs.Menu.Interact.WasPressedThisFrame();
        }
    }
}
