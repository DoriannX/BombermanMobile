using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private Vector3 _wallSize = new Vector3(80, 1, 80);
    private Vector3 _mapSize;
    private List<Vector3> _positionWalls = new List<Vector3>();
    private List<GameObject> _zoneWalls = new List<GameObject>();
    [SerializeField] private float _zoneSpeed;
    [SerializeField] private float _zoneDamage;
    [SerializeField] private float _zoneWaitTime = 10f;
    [SerializeField] private float _damageWaitTime = 1;
    private bool _canZone = false;
    private bool _canApplyDamage = true;
    public static Zone Instance;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void RemoveFromDetectedPlayer(GameObject objectToRemove)
    {
        foreach(GameObject obj in new Tools().GetAllChildren(gameObject))
        {
            List<GameObject> detectedPlayer = obj.GetComponent<CharacterDetection>().DetectedPlayers;
            if (detectedPlayer.Contains(objectToRemove))
            {
                obj.GetComponent<CharacterDetection>().DetectedPlayers.Remove(objectToRemove);
            }
            else
            {
                Debug.LogWarning("Object is not in the list");
            }
        }
    }
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
        Invoke("StartZoneAdvancement", _zoneWaitTime);
    }

    private void StartZoneAdvancement()
    {
        _canZone = true;
    }
    private void Update()
    {
        ZoneAdvancement();
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        List<GameObject> detectedPlayers = new List<GameObject>();

        foreach(Transform zoneWall in transform){
            if (zoneWall.TryGetComponent<CharacterDetection>(out CharacterDetection characterDetection))
            {
                detectedPlayers.AddRange(characterDetection.DetectedPlayers);
            }
        }
        StartCoroutine(ApplyDamage(detectedPlayers));


    }
    IEnumerator ApplyDamage(List<GameObject> units)
    {
        if (_canApplyDamage)
        {
            foreach(GameObject unit in units)
            {
                if(unit != null)
                {
                    if (unit.TryGetComponent<AIUnit>(out AIUnit AIUnit))
                    {
                        AIUnit.TakeDamage(_zoneDamage);
                        _canApplyDamage = false;
                        print("degat");
                    }
                    else
                    {
                        Debug.LogWarning("There's no AIUnit on unit");
                    }
                }
                else
                {
                    Debug.LogWarning("unit is null");
                }
            }
        }

        yield return new WaitForSeconds(_damageWaitTime);
        StopAllCoroutines();
        _canApplyDamage = true;
    }


}