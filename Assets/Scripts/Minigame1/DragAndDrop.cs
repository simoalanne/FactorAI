using Minigame1;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private ObjectSpawner _objectSpawner;
    private ObjectCollider _objectCollider;

    [SerializeField] private string targetTag;

    void Start()
    {
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
    }

    void OnMouseDown()
    {
        transform.GetComponent<SpriteRenderer>().sortingOrder += 1;
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
        foreach (Transform spawnPosition in _objectSpawner.UsedPositions)
        {
            if (spawnPosition.position == transform.position)
            {
                _objectSpawner.UsedPositions.Remove(spawnPosition);
                spawnPosition.tag = "Untagged";
                break;
            }
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        SnapToNearestPosition();
        transform.GetComponent<SpriteRenderer>().sortingOrder -= 1;

    }



    private Vector3 GetMouseWorldPos()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // Ensure z is at the same level as the object
        return mouseWorldPos;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;
            transform.position = targetPos;
        }
    }

    void SnapToNearestPosition()
    {
        Transform closestPosition = null;
        float closestDistance = Mathf.Infinity; // Start with a very large distance

        foreach (Transform spawnPosition in _objectSpawner.SpawnPositions)
        {
            float distance = (spawnPosition.position - transform.position).sqrMagnitude;
            if (distance < closestDistance)
            {
                // Check if the spawn point is used
                bool isUsed = _objectSpawner.UsedPositions.Contains(spawnPosition);

                // If the spawn point is not used or is used but has the correct tag, then consider it as a potential closest position
                if (!isUsed || (isUsed && spawnPosition.CompareTag(targetTag)))
                {
                    closestDistance = distance;
                    closestPosition = spawnPosition;
                }
            }
        }

        transform.position = closestPosition.position;
        if (!_objectSpawner.UsedPositions.Contains(closestPosition))
        {
            _objectSpawner.UsedPositions.Add(closestPosition);
        }
    }
}