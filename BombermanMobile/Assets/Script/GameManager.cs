using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool _battleStarted = false; public bool BattleStarted { get { return _battleStarted; } }

    [HideInInspector] public UnityEvent BattleStartEvent;

    private void Awake()
    {
        Instance = this;
    }

    public void StartBattle()
    {
        _battleStarted = true;
        BattleStartEvent.Invoke();
    }
}
