using System.Collections;
using UnityEngine;

namespace MiniGame2
{
    public class ManageSprites : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _dropThese;
        [SerializeField] private float _dropDelay;
        [SerializeField] private float _gravityScale = 1f;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
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

            _dropThese[0].transform.position = new Vector2(randomX1, yPosition);
            _dropThese[0].enabled = true;

            _dropThese[1].transform.position = new Vector2(randomX2, yPosition);
            _dropThese[1].enabled = true;
        }

        private IEnumerator SetGravityAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            foreach (SpriteRenderer spriteRenderer in _dropThese)
            {
                Rigidbody2D[] bodies = spriteRenderer.GetComponentsInChildren<Rigidbody2D>();
                foreach (Rigidbody2D body in bodies)
                {
                    body.gravityScale = _gravityScale;
                }
            }
        }
    }
}
