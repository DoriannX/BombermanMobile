using UnityEngine;

public class AIUnit : Unit
{
    protected private Team _team;

    protected private float _health;
    protected private float _maxHealth;

    protected private float _speed;

    protected private bool _isActivated = false;

    protected private GameObject _currentTarget; public GameObject CurrentTarget {  get { return _currentTarget; } }
    
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
        _isActivated = true;
    }

    public virtual void Attack()
    {

    }

    public virtual void TakeDamage(float damage)
    {
        _health = Mathf.Clamp(_health -= damage, 0, _maxHealth);
        if (_health <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
