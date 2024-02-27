using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoad;
        private InputReader _inputReader;

        private void Awake()
        {
            _inputReader = FindObjectOfType<InputReader>();
            if (_inputReader == null)
            {
                Debug.LogError("Error: InputReader script not found! Please add one to the scene or this won't work.");
            }
        }

        private void Update()
        {
            if (_inputReader.InteractInput)
            {
                LoadScene();
            }
        }

        private void LoadScene()
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosition2D = new Vector2(touchPosition.x, touchPosition.y);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                SceneManager.LoadScene(_sceneToLoad);
            }
        }
    }
}
