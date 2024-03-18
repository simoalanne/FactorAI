using UnityEngine;
using UnityEngine.SceneManagement;

namespace Factory
{
    public class RaycastManager : MonoBehaviour
    {
        private InputReader _inputReader;

        [SerializeField] private GameObject _minigame1Collider;
        [SerializeField] private GameObject _minigame2Collider;
        [SerializeField] GameObject _minigame1Canvas;
        [SerializeField] GameObject _minigame2Canvas;


        private void Start()
        {
            _inputReader = GetComponent<InputReader>();
        }

        private void Update()
        {
            if (_inputReader.InteractInput)
            {
                Debug.Log("Input Detected");
                Raycast();
            }
        }

        private void Raycast()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == _minigame1Collider)
                {
                    Debug.Log("mg1");
                    _minigame2Canvas.SetActive(false);
                    _minigame1Canvas.SetActive(true);
                }
                else if (hit.collider.gameObject == _minigame2Collider)
                {
                    Debug.Log("mg2");
                    _minigame1Canvas.SetActive(false);
                    _minigame2Canvas.SetActive(true);
                }
            }
        }
    }
}
