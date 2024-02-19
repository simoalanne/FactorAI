using UnityEngine;

public class ObjectCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject replacementObject;
    private bool isReplaced = false;

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