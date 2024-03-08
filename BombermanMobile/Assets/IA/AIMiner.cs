using UnityEngine;

public class AIMiner : AIUnit
{
    PathFinding _pathFinding;

    public override GameObject GetTarget()
    {
        GameObject target = _pathFinding.GetClosestWall(gameObject, CurrentTeam);

        if (target != null && _isActivated && !_isDead)
        {
            _agent.destination = target.transform.position;
            _currentTarget = target;
        }
        else
        {
            _agent.destination = transform.position;
        }
        return target;
    }

    public override void Start()
    {
        base.Start();
        _pathFinding = GetComponent<PathFinding>();
    }

    public override void Update()
    {
        base.Update();
        if (_currentTarget)
        {
            if (_pathFinding.HasWallInFront(2))
            {
                Attack();
            }
            if(_agent.velocity.magnitude <= 0)
            {
                transform.LookAt(CurrentTarget.transform.position);
            }
        }
    }

    public override void Attack()
    {
        if (CanAttack())
        {
            print("JAI POSE UNE BOMBE SUR TOI MEC");
            GameObject bomb = Instantiate(UnitManager.Instance.BombObject, gameObject.transform.position
                + (_bombPlacementOffset.z * transform.forward)
                + (_bombPlacementOffset.x * transform.right), Quaternion.identity);
            bomb.GetComponent<Bomb>().TimeToExplode = _bombTimeToExplode;
            bomb.GetComponent<Bomb>().ExplosionRange = _bombRange;
            bomb.GetComponent<Bomb>().ExplosionDamage = BombDamage;
            bomb.GetComponent<Bomb>().CurrentTeam = CurrentTeam;
            bomb.GetComponent<Bomb>().OwnerName = gameObject.name;
            StartCoroutine(Reloading());
        }
    }
}
