using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Unit
{
    public static UnitManager Instance;
    public List<GameObject> EnemiesUnits { get; private set; }
    public Type UnitType = Type.Classic;
    public Team UnitTeam = Team.Player;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    public GameObject GetClosest(GameObject unit)
    {
        
        GameObject closest = EnemiesUnits[0];

        foreach(GameObject enemyUnit in EnemiesUnits)
        {
            if(Vector3.Distance(unit.transform.position, enemyUnit.transform.position) < Vector3.Distance(unit.transform.position, closest.transform.position))
            {
                closest = enemyUnit;
            }
        }
        print(closest);
        return closest;
    }

    public GameObject GetFarthest(GameObject unit)
    {
        GameObject farthest = EnemiesUnits[0];
        foreach (GameObject enemyUnit in EnemiesUnits)
        {
            if (Vector3.Distance(unit.transform.position, enemyUnit.transform.position) > Vector3.Distance(unit.transform.position, farthest.transform.position))
            {
                farthest = enemyUnit;
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
}
