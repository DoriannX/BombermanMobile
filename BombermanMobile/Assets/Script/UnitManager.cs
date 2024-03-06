using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Unit
{
    public static UnitManager Instance;
    [HideInInspector] public List<GameObject> EnemiesUnits = new List<GameObject>();
    [HideInInspector] public List<GameObject> AllyUnits = new List<GameObject>();
    [SerializeField] private GameObject _units;
    [SerializeField] private List<Material> _materials;
    public GameObject BombObject;
    public GameObject MineObject;

    [SerializeField] private List<GameObject> _unitsToSpawn;

    public GameObject GetUnitToSpawn(Type type)
    {
        GameObject unitToSpawn = null;
        if(_unitsToSpawn.Count >= 0)
        {
            unitToSpawn = _unitsToSpawn[(int)type];
        }
        else
        {
            Debug.LogError("You forgot to put Units to spawn in the serialize field");
        }

        return unitToSpawn;
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        //StartGame();
    }
    public List<GameObject> SpawnUnit(Team unitTeam, int nb, Vector3 position, GameObject unitToSpawn)
    {
        List<GameObject> spawnedUnits = new List<GameObject>();
        for(int i = 0; i < nb; i++)
        {
            GameObject spawnedUnit = Instantiate(unitToSpawn, position, Quaternion.identity, _units.transform);
            spawnedUnit.GetComponent<AIUnit>().CurrentTeam = unitTeam;
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
                    spawnedUnit.transform.Rotate(Vector3.down*180);
                }
            }
            else
            {
                Debug.LogError("There's no material");
            }
            PathFinding pathFinding = spawnedUnit.GetComponent<PathFinding>();
        }
        return spawnedUnits;
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

    
}
