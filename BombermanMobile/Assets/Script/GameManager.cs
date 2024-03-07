using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Unit
{
    public static GameManager Instance;
    private bool _battleStarted = false; public bool BattleStarted { get { return _battleStarted; } }

    [HideInInspector] public UnityEvent BattleStartEvent;
    [SerializeField] private GameObject _buttonStart;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _text;
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
        if (_buttonStart)
        {
            _buttonStart.SetActive(false);
            _battleStarted = true;
            BattleStartEvent.Invoke();
        }
        else
        {
            Debug.LogError("Button start is empty");
        }
        if (_panel)
        {
            _panel.SetActive(false);
        }
        else
        {
            Debug.LogError("panel placement is empty");
        }
    }

    private void Update()
    {
        if (_battleStarted && UnitManager.Instance.EnemiesUnits.Count == 0)
        {
            Win();
        }
    }

    public void Lose()
    {
        print("you lose");
        Time.timeScale = 0f;
    }

    public void Win()
    {
        print("you win");
        Time.timeScale = 0f;
    }

    public void Skip()
    {
        if(_text != null)
        {
            switch (Time.timeScale)
            {
                case 1.0f:
                    Time.timeScale = 2.0f;
                    break;
                case 2.0f:
                    Time.timeScale = 3.0f;
                    break;
                case 3.0f:
                    Time.timeScale = 4.0f;
                    break;
                default:
                    Time.timeScale = 1.0f;
                    break;
            }
        }
        else
        {
            Debug.LogError("Text times is empty");
        }
    }
}
