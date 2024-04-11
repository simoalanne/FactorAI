using Minigame1;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private ObjectSpawner _objectSpawner;
    private GameStatsManager _gameStatsManager;

    [SerializeField] private string _targetTag;
    [SerializeField] private string _replacementTag;

    void Start()
    {
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
        _gameStatsManager = FindObjectOfType<GameStatsManager>();
    }

    void OnMouseDown()
    {
        transform.GetComponent<SpriteRenderer>().sortingOrder += 1;
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
        transform.localScale *= new Vector2(1.25f, 1.25f);

        Transform currentTransform = null;
        foreach (var pair in _objectSpawner.OccupiedPositions)
        {
            if (pair.Value == gameObject)
            {
                currentTransform = pair.Key;
                break;
            }
        }
        if (currentTransform != null)
        {
            _objectSpawner.OccupiedPositions.Remove(currentTransform);
        }
    }

    void OnMouseUp()
    {
        transform.localScale /= new Vector2(1.25f, 1.25f);
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
        if (Time.timeScale == 0) return;
        
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
                if (!isOccupied || (isOccupied && _objectSpawner.OccupiedPositions[gridPosition].CompareTag(_targetTag)))
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
            float scoreValue = 1000;
            if (_replacementTag == "Shovel")
            {
                _gameStatsManager.IncreaseCompletedProducts();
                scoreValue *= 2;
            }

            _gameStatsManager.IncreaseScore(scoreValue, closestPosition);
            GameObject existingObject = _objectSpawner.OccupiedPositions[closestPosition];
            _objectSpawner.OccupiedPositions.Remove(closestPosition);
            Destroy(gameObject);
            Destroy(existingObject);

            _objectSpawner.SpawnReplacementObject(closestPosition, _replacementTag);
        }
    }
}