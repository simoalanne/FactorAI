using System.Collections;
using UnityEngine;

namespace MiniGame1
{
    public class ManageSprites : MonoBehaviour
    {
        [SerializeField] private float _dropDelay;
        [SerializeField] private SpriteRenderer[] _dropThese;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;

            if (mainCamera == null)
            {
                Debug.LogError("Main camera not found!");
                return;
            }

            SetSpritePositions();
            StartCoroutine(SetGravityAfterDelay(_dropDelay));
        }

        private void SetSpritePositions()
        {
            float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
            float randomX1 = Random.Range(-cameraWidth + 1, cameraWidth - 1);
            float randomX2 = Random.Range(-cameraWidth + 1, cameraWidth - 1);
            float cameraHeight = mainCamera.orthographicSize;
            float yPosition = cameraHeight;

            // Assuming you have two sprites to set positions for
            if (_dropThese != null && _dropThese.Length >= 2)
            {
                // Set position for sprite 1
                _dropThese[0].transform.position = new Vector2(randomX1, yPosition);
                _dropThese[0].enabled = true;

                // Set position for sprite 2
                _dropThese[1].transform.position = new Vector2(randomX2, yPosition);
                _dropThese[1].enabled = true;
            }
            else
            {
                Debug.LogError("DropThese array not properly initialized or insufficient elements.");
            }
        }

        private IEnumerator SetGravityAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            foreach (SpriteRenderer spriteRenderer in _dropThese)
            {
                Rigidbody2D[] bodies = spriteRenderer.GetComponentsInChildren<Rigidbody2D>();
                foreach (Rigidbody2D body in bodies)
                {
                    body.gravityScale = 0.5f;
                }
            }
        }
    }
}
