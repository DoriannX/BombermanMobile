using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private Vector3 _wallSize = new Vector3(80, 1, 80);
    private Vector3 _mapSize;
    private List<Vector3> _positionWalls = new List<Vector3>();
    private List<GameObject> _zoneWalls = new List<GameObject>();
    [SerializeField] private float _zoneSpeed;
    [SerializeField] private float _zoneDamage;
    private bool _canZone = false;
    private bool _canApplyDamage = true;

    private void Start()
    {
        SetZoneWallsPos();
    }

    private List<GameObject> GetAllZoneWalls()
    {
        List<GameObject> zoneWalls = new List<GameObject>();

        foreach(Transform zoneWall in transform)
        {
            zoneWalls.Add(zoneWall.gameObject);
        }

        return zoneWalls;
    }

    private void SetZoneWallsPos()
    {
        _zoneWalls = GetAllZoneWalls();
        _mapSize = MapManager.Instance.MapGround.localScale;
        _positionWalls.Add(new Vector3(0, 0, -_mapSize.z*10 / 2 - _wallSize.z / 2));
        _positionWalls.Add(new Vector3(0, 0, _mapSize.z * 10 / 2 + _wallSize.z / 2));
        if (_zoneWalls.Count == 2)
        {
            for (int i = 0; i < _zoneWalls.Count; i++)
            {
                _zoneWalls[i].transform.position = _positionWalls[i];
            }
        }
        else
        {
            Debug.LogError("There's something wrong with the list of walls");
        }
    }

    private void ZoneAdvancement()
    {
        foreach(GameObject zoneWall in _zoneWalls)
        {
            if (_canZone)
            {
                zoneWall.transform.position -= zoneWall.transform.position.normalized * Time.deltaTime * _zoneSpeed / 100;
            }
        }
        Invoke("StartZoneAdvancement", 10);
    }

    private void StartZoneAdvancement()
    {
        _canZone = true;
    }
    private void Update()
    {
        ZoneAdvancement();
    }

    private void OnTriggerEnter(Collider collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject)
        {
            StartCoroutine(ApplyDamage(collision.gameObject));
        }
    }

    IEnumerator ApplyDamage(GameObject unit)
    {
        if (_canApplyDamage && unit.TryGetComponent<AIUnit>(out AIUnit AIUnit))
        {
            AIUnit.TakeDamage(_zoneDamage);
            _canApplyDamage = false;
        }

        yield return new WaitForSeconds(1);
        _canApplyDamage = true;
    }


}
