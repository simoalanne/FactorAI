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
    
    [SerializeField]
    private float scoreValue = 1000;

    private GameStatsManager _gameStatsManager;
    private Objectspawner _objectspawner;

    void Start()
    {
        _gameStatsManager = FindObjectOfType<GameStatsManager>();
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
                    _gameStatsManager.IncreaseCompletedProducts();
                    _objectspawner.SpawnObjects();
                }

                _gameStatsManager.IncreaseScore(scoreValue);
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