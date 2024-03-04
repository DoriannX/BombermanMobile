using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : Unit
{
    private void Update()
    {
        print(":)");
        UnitManager.Instance.SpawnUnit(Team.Player, 1, Vector3.zero, UnitManager.Instance.GetUnitToSpawn(Type.Funky));
    }
}
