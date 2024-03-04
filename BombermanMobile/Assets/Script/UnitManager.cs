using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class UnitManager : Unit
{
    public static UnitManager Instance;
    private List<GameObject> EnemiesUnits = new List<GameObject>();
    private List<GameObject> AllyUnits = new List<GameObject>();
    [SerializeField] private GameObject _units;
    [SerializeField] private GameObject _unitTemplate;
    [SerializeField] private List<Material> _materials;
    private Vector3 _allyPos = new Vector3(0, 1, -13);
    private Vector3 _enemyPos = new Vector3(0, 1, 13);
    private bool _canGetRandomPos = true;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        StartGame();
    }
    public List<GameObject> SpawnUnit(Team unitTeam, int nb,  Type unitType, Vector3 position)
    {
        List<GameObject> spawnedUnits = new List<GameObject>();
        for(int i = 0; i < nb; i++)
        {
            GameObject spawnedUnit = Instantiate(_unitTemplate, position, Quaternion.identity, _units.transform);
            spawnedUnits.Add(spawnedUnit);
            if (_materials.Count > 0)
            {
                if (unitTeam == Team.Player)
                {
                    AllyUnits.Add(spawnedUnit);
                    spawnedUnit.GetComponent<MeshRenderer>().material = _materials[0];
                }
                else
                {
                    EnemiesUnits.Add(spawnedUnit);
                    spawnedUnit.GetComponent<MeshRenderer>().material = _materials[1];
                }
            }
            else
            {
                Debug.LogError("There's no material");
            }
            PathFinding pathFinding = spawnedUnit.GetComponent<PathFinding>();
            pathFinding._unitTeam = unitTeam;
            pathFinding._unitType = unitType;
        }
        return spawnedUnits;
    }
    private void StartGame()
    {
        if(_units != null)
        {
            if(_unitTemplate != null)
            {
                //SpawnUnit(Team.Player, 1, Type.Classic, _allyPos);
                //SpawnUnit(Team.Ennemy, 10, Type.Classic, _enemyPos);
            }
            else
            {
                Debug.LogError("Unit template is null");
            }
        }
        else
        {
            Debug.LogError("Units is null");
        }
    }

    public void StartFight()
    {
        foreach(GameObject unit in AllyUnits)
        {
            unit.GetComponent<PathFinding>().MoveTo();
        }
        foreach(GameObject unit in EnemiesUnits)
        {
            if(unit != null)
            {
                unit.GetComponent<PathFinding>().MoveTo();
            }
            else
            {
                Debug.LogWarning("unit is null");
            }
        }
    }

    public GameObject GetClosest(GameObject unit, Team unitTeam)
    {
        List<GameObject> opponentUnit = (unitTeam == Team.Player) ? EnemiesUnits : AllyUnits;
        GameObject closest = null;
        if ( opponentUnit.Count > 0 )
        {
            closest = opponentUnit[0];
            foreach (GameObject targetUnit in opponentUnit)
            {
                if (Vector3.Distance(unit.transform.position, targetUnit.transform.position) < Vector3.Distance(unit.transform.position, closest.transform.position))
                {
                    closest = targetUnit;
                }
            }
        }
        else
        {
            Debug.LogError("opponentUnit is empty");
        }
        return closest;
    }

    public GameObject GetFarthest(GameObject unit, Team unitTeam)
    {
        List<GameObject> opponentUnit = (unitTeam == Team.Player) ? EnemiesUnits : AllyUnits;
        GameObject farthest = opponentUnit[0];
        foreach (GameObject targetUnit in EnemiesUnits)
        {
            if (Vector3.Distance(unit.transform.position, targetUnit.transform.position) > Vector3.Distance(unit.transform.position, farthest.transform.position))
            {
                farthest = targetUnit;
            }
        }
        return farthest;
    }

    public GameObject GetRandom(GameObject unit)
    {
        GameObject random = EnemiesUnits[Random.Range(0, EnemiesUnits.Count)];
        return random;
    }

    public void GetRandomPosition(NavMeshAgent agent)
    {
        StartCoroutine(RandPosCo(agent));
    }

    private IEnumerator RandPosCo(NavMeshAgent agent)
    {
        if (_canGetRandomPos)
        {
            _canGetRandomPos = false;
            Debug.Log("At Destination");
            agent.destination = new Vector3(Random.Range(
                -MapManager.Instance.MapGround.localScale.x * 10 / 2, MapManager.Instance.MapGround.localScale.x * 10 / 2),
                1,
                Random.Range(-MapManager.Instance.MapGround.localScale.z * 10 / 2, MapManager.Instance.MapGround.localScale.z * 10 / 2));
            print("random pos : " + agent.destination);
        }

        yield return new WaitForSeconds(3);
        StopAllCoroutines();
        _canGetRandomPos = true;
    }
}
