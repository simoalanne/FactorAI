using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ObjectCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject replacementObject;
    [SerializeField]
    private string TargetTag;
    private bool isReplaced = false;
    private ScoreManager _scoreManager;

    [SerializeField]
    private int scoreValue = 10; // Score value to add when objects are destroyed

    [SerializeField] private Objectspawner _objectspawner;

    void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
        _objectspawner = FindObjectOfType<Objectspawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isReplaced && collision.gameObject.CompareTag(TargetTag))
        {
            ObjectCollider otherObjectCollision = collision.gameObject.GetComponent<ObjectCollider>();
            if (otherObjectCollision != null && !otherObjectCollision.IsReplaced())
            {
                isReplaced = true;
                otherObjectCollision.SetReplaced(true);
                Destroy(collision.gameObject);
                Destroy(gameObject);
                Instantiate(replacementObject, transform.position, Quaternion.identity);

                if (replacementObject.CompareTag("Shovel"))
                {
                    _objectspawner.SpawnObjects();
                }
                // Add score when objects are destroyed
                _scoreManager.AddScore(scoreValue);
            }
        }
    }

    public bool IsReplaced()
    {
        return isReplaced;
    }

    public void SetReplaced(bool replaced)
    {
        isReplaced = replaced;
    }
}