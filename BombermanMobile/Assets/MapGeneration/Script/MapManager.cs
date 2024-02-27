using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    [SerializeField] public Transform _mapGround;

    [SerializeField] private GameObject _wallObject;

    [SerializeField] private Vector2 _mapSize = new Vector2(20, 35);
    [SerializeField] private float _tileSize = 2f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _mapSize = new Vector2(_mapGround.localScale.x * 10, _mapGround.localScale.z * 10);
        GenerateMap();
    }
    
    private void GenerateMap()
    {
        BuildWalls();
        //bake navmesh
        PlaceEnemies();
    }

    private void BuildWalls()
    {
        float halfTile = (_tileSize / 2f);
        Vector3 _placingPosition = new Vector3(-_mapSize.x/2 + halfTile, 1f, _mapSize.y/2 - halfTile) + _mapGround.position;
        List<Vector2> _walls = new List<Vector2>();
        while (_placingPosition.z > -_mapSize.y/2 - halfTile + _mapGround.position.y)
        {
            bool placedWall = false;
            int rand = Random.Range(1, 101);
            if (GetNearWallCount(_placingPosition, _walls) > 0) rand += 40;
            if (rand > 85 && !placedWall)
            {
                Instantiate(_wallObject, _placingPosition, Quaternion.identity);
                placedWall = true;
            }
            
            
            if (placedWall)
            {
                _walls.Add(new Vector2(_placingPosition.x, _placingPosition.z));
            }
            
            _placingPosition.x += _tileSize;
            if (_placingPosition.x > _mapSize.x / 2 + _mapGround.position.x)
            {
                _placingPosition.x = -_mapSize.x / 2 + halfTile;
                _placingPosition.z -= _tileSize;
            }
            
        }
    }
    
    private int GetNearWallCount(Vector3 searchPosition, List<Vector2> wallList)
    {
        int wallCount = 0;

        if (wallList.Contains(new Vector2(searchPosition.x + _tileSize, searchPosition.z)))
            wallCount += 1;

        if (wallList.Contains(new Vector2(searchPosition.x - _tileSize, searchPosition.z)))
            wallCount += 1;

        if (wallList.Contains(new Vector2(searchPosition.x, searchPosition.z + _tileSize)))
            wallCount += 1;

        if (wallList.Contains(new Vector2(searchPosition.x, searchPosition.z - _tileSize)))
            wallCount += 1;

        return wallCount;
    }

    private void PlaceEnemies()
    {

    }
}
