using UnityEngine;

namespace Minigame1
{
    public class CreateGrid : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject _borderTile;
        [SerializeField] private GameObject _gridtile;

        [Header("Grid Border Thickness as Units")]
        [SerializeField, Tooltip("How thick the border should be")] private float _gridBorderThickness = 0.5f;


        [Header("Grid Border Size As Percentage of Screen Size")]
        [SerializeField, Tooltip("How much percentage of screen width should the grid take")] private float _gridBorderSizeX = 70f;
        [SerializeField, Tooltip("How much percentage of the screen height should the grid take")] float _gridBorderSizeY = 70f;

        [Header("Grid Border Offset As Percentage of Screen Size")]
        [SerializeField, Tooltip("Offset percentage from the middle of the screen in x-axis")] private float _gridBorderOffsetX = 0f;
        [SerializeField, Tooltip("Offset percentage from the middle of the screen in y-axis")] private float _gridBorderOffsetY = -10f;

        [Header("Grid Size in Tiles")]
        [SerializeField, Tooltip("How many tiles in x-axis")] private int _gridSizeX;
        [SerializeField, Tooltip("How many tiles in y-axis")] private int _gridSizeY;

        private Camera _camera;
        private ObjectSpawner _objectSpawner;

        private Vector2 _gridTileScale;
        public Vector2 GridTileScale => _gridTileScale;

        void Awake()
        {
            _objectSpawner = FindObjectOfType<ObjectSpawner>();
            _camera = Camera.main;
            InstantiateBorder();
            InstantiateGrid();
        }


        void InstantiateBorder()
        {
            float cameraHeightInUnits = _camera.orthographicSize * 2f;
            float cameraWidthInUnits = cameraHeightInUnits * _camera.aspect;

            float gridBorderWidth = cameraWidthInUnits * _gridBorderSizeX / 100;
            float gridBorderHeight = cameraHeightInUnits * _gridBorderSizeY / 100;

            float offsetX = cameraWidthInUnits * _gridBorderOffsetX / 100;
            float offsetY = cameraHeightInUnits * _gridBorderOffsetY / 100;

            Vector2 BorderTopSide = new Vector2(0 + offsetX, gridBorderHeight / 2 - _gridBorderThickness / 2 + offsetY);
            Vector2 BorderBottomSide = new Vector2(0 + offsetX, -gridBorderHeight / 2 + _gridBorderThickness / 2 + offsetY);

            Vector2 BorderLeftSide = new Vector2(-gridBorderWidth / 2 + _gridBorderThickness / 2 + offsetX, 0 + offsetY);
            Vector2 BorderRightSide = new Vector2(gridBorderWidth / 2 - _gridBorderThickness / 2 + offsetX, 0 + offsetY);

            Instantiate(_borderTile, BorderTopSide, Quaternion.identity, transform).transform.localScale = new Vector2(gridBorderWidth, _gridBorderThickness);
            Instantiate(_borderTile, BorderBottomSide, Quaternion.identity, transform).transform.localScale = new Vector2(gridBorderWidth, _gridBorderThickness);
            Instantiate(_borderTile, BorderLeftSide, Quaternion.identity, transform).transform.localScale = new Vector2(_gridBorderThickness, gridBorderHeight);
            Instantiate(_borderTile, BorderRightSide, Quaternion.identity, transform).transform.localScale = new Vector2(_gridBorderThickness, gridBorderHeight);
        }

        void InstantiateGrid()
        {
            float cameraHeightInUnits = _camera.orthographicSize * 2f;
            float cameraWidthInUnits = cameraHeightInUnits * _camera.aspect;

            float gridBorderWidth = cameraWidthInUnits * _gridBorderSizeX / 100;
            float gridBorderHeight = cameraHeightInUnits * _gridBorderSizeY / 100;

            float offsetX = cameraWidthInUnits * _gridBorderOffsetX / 100;
            float offsetY = cameraHeightInUnits * _gridBorderOffsetY / 100;

            float tileWidth = (gridBorderWidth - 2 * _gridBorderThickness) / _gridSizeX;
            float tileHeight = (gridBorderHeight - 2 * _gridBorderThickness) / _gridSizeY;

            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int j = 0; j < _gridSizeY; j++)
                {
                    Vector2 tilePosition = new(-gridBorderWidth / 2 + _gridBorderThickness + i * tileWidth + tileWidth / 2 + offsetX,
                                                        -gridBorderHeight / 2 + _gridBorderThickness + j * tileHeight + tileHeight / 2 + offsetY);
                    Instantiate(_gridtile, tilePosition, Quaternion.identity, transform).transform.localScale = new(tileWidth, tileHeight);

                    _objectSpawner.SpawnPositions.Add(transform.GetChild(transform.childCount - 1));
                    Debug.Log(transform.GetChild(transform.childCount - 1).localPosition);
                }

            }
            _gridTileScale = new Vector2(tileWidth, tileHeight); // Store the scale of the grid tile for later use
            _objectSpawner.SpawnObjects();
        }
    }
}





