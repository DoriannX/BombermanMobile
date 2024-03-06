using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIUnit : Unit
{
    protected private NavMeshAgent _agent;
    [SerializeField] private GameObject _playerVisuals;
    [SerializeField] private GameObject _enemyVisuals;
    [SerializeField] private GameObject _visuals;
    [HideInInspector] public Team CurrentTeam;

    protected private float _health; public float Health { get { return _health; } }
    [SerializeField] protected private float _maxHealth; public float MaxHealth { get { return _maxHealth; } }

    protected private float _speed; public float Speed { get { return _speed; } }
    [SerializeField] protected private float _maxSpeed;

    [SerializeField] protected private float _reloadingTime = 1;
    protected private bool _isReloading = false;

    protected private bool _isActivated = false; // Utiliser variable

    protected private GameObject _currentTarget; public GameObject CurrentTarget {  get { return _currentTarget; } }
    [SerializeField] private protected float _range = 1;

    [SerializeField] private protected Vector3 _bombPlacementOffset = Vector3.zero;
    public float BombDamage = 1;
    [SerializeField] private protected float _bombTimeToExplode = 1.5f;
    [SerializeField] private protected float _bombRange = 2f;

    private float _baseHeight = 0;
    private float _randomVisuals = 0; //random constant number for animations and stuff

    protected private enum AISTATES
    {
        IDLE,
    }

    protected private AISTATES _currentState;

    public virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetBaseStats();

    }

    public virtual void Start()
    {
        GameManager.Instance.BattleStartEvent.AddListener(OnStartingBattle);
        if (CurrentTeam == Team.Player)
        {
            _playerVisuals.SetActive(true);
            _enemyVisuals.SetActive(false);
        }
        else
        {
            _playerVisuals.SetActive(false);
            _enemyVisuals.SetActive(true);
        }
        _baseHeight = transform.position.y;
        _randomVisuals = Random.Range(-100, 100);
    }

    public virtual void Update()
    {
        if (_agent.velocity.magnitude > 0.5f)
        {
            _visuals.transform.position = new Vector3(_visuals.transform.position.x, transform.position.y + 1 * Mathf.Abs(Mathf.Sin((Time.time + _randomVisuals) * 15)), _visuals.transform.position.z);
        }
        else
        {
            _visuals.transform.position = new Vector3(_visuals.transform.position.x, transform.position.y, _visuals.transform.position.z);
        }
    }

    public virtual void SetBaseStats()
    {
        _health = _maxHealth;
        _speed = _maxSpeed;
        _isReloading = false;
        _agent.speed = _speed;
    }

    public virtual void OnStartingBattle()
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
        ParticleManager.Instance.ExplodeParticle(transform.position);
        if (CurrentTeam == Team.Ennemy)
            UnitManager.Instance.EnemiesUnits.Remove(gameObject);
        else
            UnitManager.Instance.AllyUnits.Remove(gameObject);
    }

    public virtual IEnumerator Reloading()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadingTime);
        _isReloading = false;
    }

    public virtual bool CanAttack()
    {
        if (!_isReloading && _isActivated) 
        {
            return true;
        }
        return false;
    }

    public virtual GameObject GetTarget()
    {
        GameObject target = null;
        return target;
    }

    private void OnDestroy()
    {
        if(Zone.Instance != null)
        {
            Zone.Instance.RemoveFromDetectedPlayer(gameObject);
        }
    }
}
