using System.Collections.Generic;
using Global;
using UnityEngine;

namespace Minigame1
{
    [System.Serializable]
    public class SpawnableObject
    {
        public GameObject ObjectToSpawn;
        public int AmountToSpawn;
        public float _percentageOfTileSize = 0.75f;
    }

    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnableObject[] _product1Objects;
        [SerializeField] private SpawnableObject[] _product2Objects;
        private readonly List<Transform> _spawnPositions = new();
        private readonly List<Transform> _usedPositions = new();

        public List<Transform> SpawnPositions => _spawnPositions;
        public List<Transform> UsedPositions => _usedPositions;

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
                    Transform randomPosition = _spawnPositions[Random.Range(0, _spawnPositions.Count)];
                    if (!_usedPositions.Contains(randomPosition))
                    {
                        // Instantiate the object first
                        GameObject instance = Instantiate(spawnableObject.ObjectToSpawn, randomPosition.position, Quaternion.identity);

                        // Then modify the properties of the instance
                        instance.transform.localScale = _createGrid.GridTileScale * spawnableObject._percentageOfTileSize;
                        instance.GetComponent<SpriteRenderer>().flipX = Random.Range(0, 2) == 0;

                        randomPosition.tag = instance.tag;
                        _usedPositions.Add(randomPosition);
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