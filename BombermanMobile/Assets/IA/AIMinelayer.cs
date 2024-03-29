using UnityEngine;

public class AIMinelayer : AIUnit
{
    PathFinding _pathFinding;
    
    public override GameObject GetTarget()
    {
        if (_isActivated && !_isDead)
        {
            _agent.destination = _pathFinding.GetRandomPosition();
        }
        else
        {
            _agent.destination = transform.position;
        }
        _currentTarget = null;
        return null;
    }

    public override void Awake()
    {
        base.Awake();
        _pathFinding = GetComponent<PathFinding>();
    }

    public override void Update()
    {
        base.Update();
        GetTarget();
        Attack();
    }

    public override void Attack()
    {
        if (CanAttack())
        {
            print("JAI POSE UNE BOMBE SUR TOI MEC");
            GameObject bomb = Instantiate(UnitManager.Instance.MineObject, gameObject.transform.position
                + (_bombPlacementOffset.z * transform.forward)
                + (_bombPlacementOffset.x * transform.right), Quaternion.identity);
            bomb.GetComponent<Bomb>().TimeToExplode = 0;
            bomb.GetComponent<Bomb>().ExplosionRange = _bombRange;
            bomb.GetComponent<Bomb>().ExplosionDamage = BombDamage;
            bomb.GetComponent<Bomb>().CurrentTeam = CurrentTeam;
            bomb.GetComponent<Bomb>().OwnerName = gameObject.name;
            StartCoroutine(Reloading());
        }
    }
}
