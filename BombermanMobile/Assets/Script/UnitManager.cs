using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        StartGame();
    }
    private List<GameObject> SpawnUnit(Team unitTeam, int nb,  Type unitType)
    {
        List<GameObject> spawnedUnits = new List<GameObject>();
        for(int i = 0; i < nb; i++)
        {
            GameObject spawnedUnit = Instantiate(_unitTemplate, _units.transform);
            spawnedUnits.Add(spawnedUnit);
            if (_materials.Count > 0)
            {
                if (unitTeam == Team.Player)
                {
                    spawnedUnit.transform.position = _allyPos;
                    AllyUnits.Add(spawnedUnit);
                    spawnedUnit.GetComponent<MeshRenderer>().material = _materials[0];
                }
                else
                {
                    spawnedUnit.transform.position = _enemyPos;
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
                SpawnUnit(Team.Player, 1, Type.Classic);
                SpawnUnit(Team.Ennemy, 10, Type.Classic);
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
            unit.GetComponent<PathFinding>().MoveTo();
        }
    }

    public GameObject GetClosest(GameObject unit, Team unitTeam)
    {
        List<GameObject> opponentUnit = (unitTeam == Team.Player) ? EnemiesUnits : AllyUnits;
        print(EnemiesUnits.Count);
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
            print(closest);
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
        print(farthest);
        return farthest;
    }

    public GameObject GetRandom(GameObject unit)
    {
        GameObject random = EnemiesUnits[Random.Range(0, EnemiesUnits.Count)];
        
        print(random);
        return random;
    }

    public Vector3 GetRandomPosition()
    {
        Vector3 randomPos = new Vector3(Random.Range(MapManager.Instance.MapGround.localScale.x * 10 / 2, MapManager.Instance.MapGround.localScale.y * 10 / 2), 1);
        return randomPos;
    }
}
