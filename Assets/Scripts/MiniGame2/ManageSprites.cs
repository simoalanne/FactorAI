using System.Collections;
using System.Linq;
using UnityEngine;
using Global;

namespace Minigame2
{
    public class ManageSprites : MonoBehaviour
    {
        [SerializeField] private GameObject[] _spawnedObjects1;
        [SerializeField] private GameObject[] _spawnedObjects2;

        [SerializeField] private float _gravityScale = 0.5f;
        [SerializeField] private float _dropIntervalInitial = 3.0f;
        [SerializeField] private float _dropIntervalFastest = 0.75f;
        [SerializeField] private int _maxConsecutiveWrongObjects = 3;
        [SerializeField] private int _preventSpawnToRecents = 5;

        public GameObject[] SpawnedObjects => _spawnedObjects1;
        public GameObject[] SpawnedObjects2 => _spawnedObjects2;

        private GameObject[] _spawnedObjects;
        private Camera _mainCamera;
        private float _cameraWidth, _yPosition;
        private bool _gameActive = true;
        private int _countConsecutiveWrongObjects;
        private int[] _recentXPositions;

        public bool GameActive => _gameActive;

        private void Awake()
        {
            if (GameManager.Instance.CurrentProduct == "Product1" || GameManager.Instance == null)
            {
                _spawnedObjects = _spawnedObjects1;
            }
            else if (GameManager.Instance.CurrentProduct == "Product2")
            {
                _spawnedObjects = _spawnedObjects2;
                _dropIntervalFastest -= 0.125f;

            }
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            _cameraWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
            _yPosition = _mainCamera.orthographicSize + 5f;

            int maxSpawnPoints = Mathf.RoundToInt(_cameraWidth * 2 - 4f);

            if (maxSpawnPoints < _preventSpawnToRecents)
            {
                Debug.LogWarning("PreventSpawnToRecents is greater than the possible spawn points. Game will freeze.");
                Debug.Log("Setting PreventSpawnToRecents to " + (maxSpawnPoints - 1) + ".");
                _preventSpawnToRecents = maxSpawnPoints - 1; // Prevents possible infinite loop that will freeze the game.
            }

            _recentXPositions = new int[_preventSpawnToRecents];

            // Now first oject spawn can be at x-coordinate zero.
            for (int i = 0; i < _recentXPositions.Length; i++)
            {
                _recentXPositions[i] = (int)_cameraWidth;
            }



            StartCoroutine(StartSpawnLoop());
        }

        private IEnumerator StartSpawnLoop()
        {
            while (GameActive)
            {
                SpawnToRandomAndApplyGravity(InstantiateRandom());

                if (_dropIntervalInitial > _dropIntervalFastest)
                {
                    _dropIntervalInitial -= 0.025f;
                }
                yield return new WaitForSeconds(_dropIntervalInitial);
            }
        }

        private GameObject InstantiateRandom()
        {
            int randomIndex = Mathf.RoundToInt(Random.Range(0, _spawnedObjects.Length));

            if (_spawnedObjects[randomIndex].CompareTag("UndesiredProduct"))
            {
                _countConsecutiveWrongObjects++;
            }

            else
            {
                _countConsecutiveWrongObjects = 0;
            }

            if (_countConsecutiveWrongObjects <= _maxConsecutiveWrongObjects)
            {
                return Instantiate(_spawnedObjects[randomIndex]);
            }

            _countConsecutiveWrongObjects = 0;
            return Instantiate(_spawnedObjects[0]);
        }

        private void SpawnToRandomAndApplyGravity(GameObject prefab)
        {
            int randomX;

            do
            {
                randomX = Mathf.RoundToInt(Random.Range(-_cameraWidth + 2f, _cameraWidth - 2f));
            } while (IsRecentXPosition(randomX));

            prefab.transform.position = new Vector2(randomX, _yPosition);

            Rigidbody2D body = prefab.GetComponentInChildren<Rigidbody2D>();
            body.gravityScale = _gravityScale;
        }

        private bool IsRecentXPosition(int xToCheck)
        {
            if (_recentXPositions.Contains(xToCheck))
            {
                return true;
            }

            for (int i = 0; i < _recentXPositions.Length - 1; i++)
            {
                _recentXPositions[i] = _recentXPositions[i + 1];
            }

            _recentXPositions[_recentXPositions.Length - 1] = xToCheck;
            return false;
        }

        public void OnDisable()
        {
            _gameActive = false;
        }
    }
}
