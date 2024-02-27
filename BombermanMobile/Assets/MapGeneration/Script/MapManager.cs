using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    [SerializeField] private Transform _mapGround;

    [SerializeField] private GameObject _wallObject;

    [SerializeField] private Vector2 _mapSize = new Vector2(20, 35);

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
        Vector3 _placingPosition = new Vector3(-_mapSize.x/2 + 1, 1f, _mapSize.y/2 - 1) + _mapGround.position;
        while (_placingPosition.z > -_mapSize.y/2 - 1 + _mapGround.position.y)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                Instantiate(_wallObject, _placingPosition, Quaternion.identity);
            }
            
            
            _placingPosition.x += 2;
            if (_placingPosition.x > _mapSize.x/2 + _mapGround.position.x)
            {
                _placingPosition.x = -_mapSize.x / 2 + 1;
                _placingPosition.z -= 2;
            }
            
        }
    }

    private void PlaceEnemies()
    {

    }
}
