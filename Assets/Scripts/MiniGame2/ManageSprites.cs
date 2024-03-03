using System.Collections;
using System.Linq;
using UnityEngine;

namespace MiniGame2
{
    public class ManageSprites : MonoBehaviour
    {
        [SerializeField] private GameObject[] _prefabArray;
        [SerializeField] private float _gravityScale = 0.5f;
        [SerializeField] private float _dropIntervalInitial = 3.0f; 
        [SerializeField] private float _dropIntervalFastest = 0.75f;
        [SerializeField] private int _maxConsecutiveWrongObjects = 3;
        [SerializeField] private int _preventSpawnToRecents = 5;
       
        private Camera _mainCamera;
        private float _cameraWidth, _yPosition;
        private bool _gameActive = true;
        private int _countConsecutiveWrongObjects;
        private int[] _recentXPositions;

        public bool GameActive => _gameActive;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _cameraWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
            _yPosition = _mainCamera.orthographicSize - 1;
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
                _dropIntervalInitial -= 0.05f;
                }
                yield return new WaitForSeconds(_dropIntervalInitial);
            }
        }

        private GameObject InstantiateRandom()
        {
            int randomIndex = Mathf.RoundToInt(Random.Range(0, _prefabArray.Length));
            
            if (_prefabArray[randomIndex].tag == "UndesiredProduct")
            {
                _countConsecutiveWrongObjects++;
                Debug.Log("TRASH SPAWNED" + _countConsecutiveWrongObjects);
            }

            else
            {
                _countConsecutiveWrongObjects = 0;
                Debug.Log("reset");
            }

            if (_countConsecutiveWrongObjects <= _maxConsecutiveWrongObjects)
            {
                return Instantiate(_prefabArray[randomIndex]);
            }

            // Reset the counter
            Debug.Log("FORCE PRODUCT");
            _countConsecutiveWrongObjects = 0;
            return Instantiate(_prefabArray[0]);
        }

        private void SpawnToRandomAndApplyGravity(GameObject prefab)
        {
            int randomX;

            do
            {
                randomX = Mathf.RoundToInt(Random.Range(-_cameraWidth + 1, _cameraWidth));
            } while (IsRecentXPosition(randomX));

            prefab.transform.position = new Vector2(randomX, _yPosition);
            
            Rigidbody2D body = prefab.GetComponentInChildren<Rigidbody2D>();
            body.gravityScale = _gravityScale;
        }

        private bool IsRecentXPosition(int xToCheck)
        {
            /*
            Some really clean code here :D
            For loop shifts the stored recent coordinates towards index 0
            and the parameter is placed as the highest index.
            */
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
    }
}
