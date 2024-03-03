using UnityEngine;

public class ObjectCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject replacementObject;
    private bool isReplaced = false;

    [SerializeField]
    private int scoreValue = 10; // Score value to add when objects are destroyed

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isReplaced && collision.gameObject.CompareTag("TargetObject"))
        {
            ObjectCollider otherObjectCollision = collision.gameObject.GetComponent<ObjectCollider>();
            if (otherObjectCollision != null && !otherObjectCollision.IsReplaced())
            {
                isReplaced = true;
                otherObjectCollision.SetReplaced(true);
                Destroy(collision.gameObject);
                Destroy(gameObject);
                Instantiate(replacementObject, transform.position, Quaternion.identity);
                
                // Add score when objects are destroyed
                ScoreManager.instance.AddScore(scoreValue);
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

    /*private void Update()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("TargetObject");
        foreach (GameObject obj in objects)
        {
            Debug.Log("Object position: " + obj.transform.position);
        }
    }*/