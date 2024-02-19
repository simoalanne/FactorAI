using UnityEngine;
using System.Collections.Generic;

public class ObjectGenerator : MonoBehaviour
{
    [System.Serializable]
    public class ObjectCount
    {
        public GameObject objectPrefab;
        public int count;
    }

    public List<ObjectCount> objectsToGenerate; // List of objects and their counts
    public float minX = -5f; // Minimum X coordinate within the camera's view
    public float maxX = 5f; // Maximum X coordinate within the camera's view
    public float minY = -3f; // Minimum Y coordinate within the camera's view
    public float maxY = 3f; // Maximum Y coordinate within the camera's view

    private Camera mainCamera;
    private List<Vector3> usedPositions = new List<Vector3>(); // List to store used positions

    void Start()
    {
        mainCamera = Camera.main;
        GenerateObjects();
    }

    void GenerateObjects()
    {
        foreach (ObjectCount objectCount in objectsToGenerate)
        {
            for (int i = 0; i < objectCount.count; i++)
            {
                // Generate random position within camera's view
                Vector3 randomPosition = GetRandomPosition();

                // Instantiate the object at the generated position
                Instantiate(objectCount.objectPrefab, randomPosition, Quaternion.identity);

                // Add position to used positions list
                usedPositions.Add(randomPosition);
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;
        do
        {
            // Generate random position within camera's view
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            randomPosition = new Vector3(randomX, randomY, 0f);
        }
        // Check if the generated position is not already used
        while (usedPositions.Contains(randomPosition));

        return randomPosition;
    }
}