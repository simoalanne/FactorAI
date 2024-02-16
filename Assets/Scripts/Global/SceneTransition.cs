using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Name of the scene to load
    public string sceneToLoad;

    private void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world space
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;

            // Check if the click position is within the square's collider
            Collider2D hitCollider = Physics2D.OverlapPoint(clickPosition);
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                // Load the specified scene
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}