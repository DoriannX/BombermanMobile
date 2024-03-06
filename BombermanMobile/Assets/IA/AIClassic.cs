using UnityEngine;

public class AIClassic : AIUnit
{
    
    PathFinding _pathFinding;

    public override GameObject GetTarget()
    {
        GameObject target = UnitManager.Instance.GetClosest(gameObject, CurrentTeam);
        if (target != null && _isActivated)
        {
            _currentTarget = target;
            if (Vector3.Distance(transform.position, _currentTarget.transform.position) > _range)
            {
                _agent.destination = target.transform.position;
            }
            else
            {
                _agent.destination = transform.position;
            }
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
            if (_pathFinding.IsCloseToEnnemy(_range, _currentTarget) || _pathFinding.HasWallInFront(2))
            {
                Attack();
                transform.LookAt(CurrentTarget.transform.position);
            }
        }
    }

    public override void Attack()
    {
        if(CanAttack())
        {
            print("JAI POSE UNE BOMBE SUR TOI MEC");
            GameObject bomb = Instantiate(UnitManager.Instance.BombObject, gameObject.transform.position
                + (_bombPlacementOffset.z * transform.forward)
                + (_bombPlacementOffset.x * transform.right), Quaternion.identity);
            bomb.GetComponent<Bomb>().TimeToExplode = _bombTimeToExplode;
            bomb.GetComponent<Bomb>().ExplosionRange = _bombRange;
            bomb.GetComponent<Bomb>().ExplosionDamage = BombDamage;
            bomb.GetComponent<Bomb>().CurrentTeam = CurrentTeam;
            StartCoroutine(Reloading());
        }
    }

}
