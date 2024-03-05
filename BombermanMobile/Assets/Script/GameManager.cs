using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Unit
{
    public static GameManager Instance;
    private bool _battleStarted = false; public bool BattleStarted { get { return _battleStarted; } }

    [HideInInspector] public UnityEvent BattleStartEvent;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UnitManager.Instance.SpawnUnit(Team.Player, 1, new Vector3(0, 0, -14.3000002f), UnitManager.Instance.GetUnitToSpawn(Type.Goass));
        UnitManager.Instance.SpawnUnit(Team.Ennemy, 1, new Vector3(0, 0, 14.3000002f), UnitManager.Instance.GetUnitToSpawn(Type.Goass));
    }

    public void StartBattle()
    {

        _battleStarted = true;
        BattleStartEvent.Invoke();
    }
}
