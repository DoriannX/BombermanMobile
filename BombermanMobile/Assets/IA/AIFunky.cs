using UnityEngine;

public class AIFunky : AIUnit
{
    PathFinding _pathFinding;

    public override GameObject GetTarget()
    {
        GameObject target = null;
        target = UnitManager.Instance.GetClosest(gameObject, CurrentTeam);

        
        if (target != null)
        {
            _currentTarget = target;
            if (_pathFinding.IsCloseToEnnemy(_range, _currentTarget) && _isActivated)
            {
                _agent.destination = transform.position + -(target.transform.position - transform.position).normalized * _range;
                //flee
            }
            else
            {
                _agent.destination = target.transform.position;
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
            if (_pathFinding.HasWallInFront(2))
            {
                Attack();
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
            StartCoroutine(Reloading());
        }
    }

}
