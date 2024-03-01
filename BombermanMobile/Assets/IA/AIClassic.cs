using UnityEngine;

public class AIClassic : AIUnit
{
    
    PathFinding _pathFinding;

    public override Vector3 GetTargetPosition()
    {
        GameObject target = UnitManager.Instance.GetClosest(gameObject, CurrentTeam);

        if (target != null)
        {
            _agent.destination = target.transform.position;
            _currentTarget = target;
        }
        return target.transform.position;
    }

    public override void Start()
    {
        base.Start();
        _pathFinding = GetComponent<PathFinding>();
    }

    private void Update()
    {
        if (_currentTarget)
        {
            if (_pathFinding.IsCloseToEnnemy(_range, _currentTarget))
            {
                Attack();
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
            bomb.GetComponent<Bomb>().ExplosionDamage = _bombDamage;
            bomb.GetComponent<Bomb>().CurrentTeam = CurrentTeam;
            StartCoroutine(Reloading());
        }
        //pose ta bombe fdp
    }

}
