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

        public SpawnableObject[] Product1Objects => _product1Objects;
        public SpawnableObject[] Product2Objects => _product2Objects;
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

        public void SpawnReplacementObject(Transform pos, string replacementTag)
        {
            foreach (SpawnableObject spawnableObject in _product1Objects)
            {
                if (spawnableObject.ObjectToSpawn.CompareTag(replacementTag))
                {
                    GameObject instance = Instantiate(spawnableObject.ObjectToSpawn, pos.position, Quaternion.identity);
                    instance.transform.localScale = _createGrid.GridTileScale * (spawnableObject._percentageOfTileSize / 100f);
                    _occupiedPositions.Add(pos, instance);
                    if (replacementTag == "Shovel")
                    {
                        SpawnObjects();
                        _occupiedPositions.Remove(pos);
                    }
                    break; // This will exit the loop after the first matching object is spawned
                }
            }
        }
    }
}