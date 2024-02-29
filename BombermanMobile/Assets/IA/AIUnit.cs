using UnityEngine;

public class AIUnit : Unit
{
    protected private Team _team;

    protected private float _health;
    protected private float _maxHealth;

    protected private float _speed;
    
    protected private enum AISTATES
    {
        IDLE,
    }

    protected private AISTATES _currentState;

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        GameManager.Instance.BattleStartEvent.AddListener(StartingBattle);
    }

    public virtual void StartingBattle()
    {

    }

    public virtual void Attack()
    {

    }
}
