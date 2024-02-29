using UnityEngine;

public class AIClassic : AIUnit
{
    
    PathFinding _pathFinding;

    public override Vector3 GetTarget()
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
        if (_pathFinding.IsCloseToEnnemy(_range, _currentTarget))
        {
            Attack();
        }
    }

    public override void Attack()
    {
        if(CanAttack())
        {
            print("JAI POSE UNE BOMBE SUR TOI MEC");
            Reloading();
        }
        //pose ta bombe fdp
    }

}
