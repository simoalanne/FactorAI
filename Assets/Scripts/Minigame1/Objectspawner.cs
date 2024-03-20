using UnityEngine;

[System.Serializable]
public class ObjectToSpawn
{
    public GameObject prefab;
    public int amountToSpawn;
}

public class Objectspawner : MonoBehaviour
{
    public ObjectToSpawn[] objectsToSpawn; // Array of objects to spawn
    public Transform[] spawnPoints; // Array of spawn points

    void Start()
    {
        SpawnObjects();
    }

    public void SpawnObjects()
    {
        foreach (ObjectToSpawn objToSpawn in objectsToSpawn)
        {
            for (int i = 0; i < objToSpawn.amountToSpawn; i++)
            {
                // Randomly select a spawn point
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[spawnIndex];

                // Check if the spawn point is available
                if (!IsSpawnPointOccupied(spawnPoint.position))
                {
                    // Spawn the object at the chosen spawn point
                    Instantiate(objToSpawn.prefab, spawnPoint.position, Quaternion.identity);
                }
                else
                {
                    // If the spawn point is occupied, try again
                    i--;
                }
            }
        }
    }

    bool IsSpawnPointOccupied(Vector3 position)
    {
        // Check if any object is already spawned at the given position
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f); // Adjust the radius as needed
        return colliders.Length > 0;
    }
}
