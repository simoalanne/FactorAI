using UnityEngine;

namespace Minigame1
{
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
    private ObjectSpawner _objectspawner;

    void Start()
    {
        _gameStatsManager = FindObjectOfType<GameStatsManager>();
        _objectspawner = FindObjectOfType<ObjectSpawner>();
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
}