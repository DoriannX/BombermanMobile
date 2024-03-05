using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Unit
{
    public static MapManager Instance;
    [SerializeField] private Transform _mapGround; public Transform MapGround {  get { return _mapGround; } }

    [SerializeField] private GameObject _wallObject;

    [SerializeField] private Vector2 _mapSize = new Vector2(20, 35);
    [SerializeField] private float _tileSize = 2f;
    public List<GameObject> Bonuses = new List<GameObject>();


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

        int nbUnit = UnityEngine.Random.Range(5, 8);

        for (int i = 0; i < nbUnit; i++)
        {
            PlaceEnemies();
        }
    }

    private void BuildWalls()
    {
        float halfTile = (_tileSize / 2f);
        Vector3 _placingPosition = new Vector3(-_mapSize.x/2 + halfTile, 1f, _mapSize.y/2 - halfTile) + _mapGround.position;
        List<Vector2> _walls = new List<Vector2>();
        while (_placingPosition.z > -_mapSize.y/2 - halfTile + _mapGround.position.y)
        {
            bool placedWall = false;
            int rand = UnityEngine.Random.Range(1, 101);
            if (GetNearWallCount(_placingPosition, _walls) > 0) rand += 40;
            if (rand > 85 && !placedWall)
            {
                Instantiate(_wallObject, _placingPosition, Quaternion.identity, transform);
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
        bool placedEnemy = false;
        Vector3 randomPos = new Vector3();
        int iterationMax = 1000;
        int unitType = 0;
        while (!placedEnemy)
        {
            bool obstacleDetected = false;
            unitType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(Type)).Length - 1);
            if(unitType == 6) 
            {
                obstacleDetected = true;
            }
            randomPos = new Vector3(UnityEngine.Random.Range(
                    -MapManager.Instance.MapGround.localScale.x * 10 / 2, MapManager.Instance.MapGround.localScale.x * 10 / 2),
                    1,
                    UnityEngine.Random.Range(-MapManager.Instance.MapGround.localScale.z * 10 / 2, MapManager.Instance.MapGround.localScale.z * 10 / 2));

            foreach (Collider collider in Physics.OverlapSphere(randomPos, .8f))
            {
                if (!collider.CompareTag("Ground"))
                {
                    obstacleDetected = true;
                    print("Obstacle Detected");
                }
            }
            if (!obstacleDetected)
            {
                placedEnemy = true;
                print("Enemy detected");
            }
            iterationMax--;
            if (iterationMax <= 0)
            {
                placedEnemy = true;
                break;
            }
        }
        UnitManager.Instance.SpawnUnit(Team.Ennemy, 1, randomPos, UnitManager.Instance.GetUnitToSpawn((Type)unitType));
    }
}
