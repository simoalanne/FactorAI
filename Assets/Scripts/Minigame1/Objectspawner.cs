using System.Collections.Generic;
using UnityEngine;

namespace Minigame1
{
    [System.Serializable]
    public class SpawnableObject
    {
        public GameObject ObjectToSpawn;
        public int AmountToSpawn;
        public float _percentageOfTileSize = 75f;
    }

    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnableObject[] _product1Objects;
        [SerializeField] private SpawnableObject[] _product2Objects;
        private readonly List<Transform> _availablePositions = new();
        private readonly Dictionary<Transform, GameObject> _occupiedPositions = new();

        public List<Transform> AvailablePositions => _availablePositions;
        public Dictionary<Transform, GameObject> OccupiedPositions => _occupiedPositions;

        private CreateGrid _createGrid;

        void Awake()
        {
            _createGrid = FindObjectOfType<CreateGrid>();
        }

        public void SpawnObjects()
        {
            foreach (SpawnableObject spawnableObject in _product1Objects)
            {
                for (int i = 0; i < spawnableObject.AmountToSpawn; i++)
                {
                    Transform randomPosition = _availablePositions[Random.Range(0, _availablePositions.Count)];
                    if (!_occupiedPositions.ContainsKey(randomPosition))
                    {
                        // Instantiate the object first
                        GameObject instance = Instantiate(spawnableObject.ObjectToSpawn, randomPosition.position, Quaternion.identity);

                        // Then modify the properties of the instance
                        instance.transform.localScale = _createGrid.GridTileScale * (spawnableObject._percentageOfTileSize / 100f);
                        instance.GetComponent<SpriteRenderer>().flipX = Random.Range(0, 2) == 0;

                        // Add the instance to the dictionary with the randomPosition as the key
                        _occupiedPositions.Add(randomPosition, instance);
                    }
                    else
                    {
                        i--;
                    }
                }
            }
        }
    }
}