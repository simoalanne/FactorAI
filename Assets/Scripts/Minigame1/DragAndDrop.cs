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
        Transform currentTransform = transform;
        if (_objectSpawner.OccupiedPositions.ContainsKey(currentTransform))
        {
            _objectSpawner.OccupiedPositions.Remove(currentTransform);
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

        foreach (Transform gridPosition in _objectSpawner.AvailablePositions)
        {
            float distance = Vector2.Distance(gridPosition.position, transform.position);
            if (distance < closestDistance)
            {
                // Check if the grid point is occupied
                bool isOccupied = _objectSpawner.OccupiedPositions.ContainsKey(gridPosition);

                // If the grid point is not occupied or is occupied but contains gameobject with correct tag, then consider it as a potential closest position
                if (!isOccupied || (isOccupied && _objectSpawner.OccupiedPositions[gridPosition].CompareTag(targetTag)))
                {
                    closestDistance = distance;
                    closestPosition = gridPosition;
                }
            }
        }

        transform.position = closestPosition.position;
        if (!_objectSpawner.OccupiedPositions.ContainsKey(closestPosition))
        {
            _objectSpawner.OccupiedPositions.Add(closestPosition, gameObject);
        }
        else
        {
            GameObject existingObject = _objectSpawner.OccupiedPositions[closestPosition];
            _objectSpawner.OccupiedPositions.Remove(closestPosition);
            Destroy(gameObject);
            Destroy(existingObject);
        }
    }
}