using UnityEngine;

public class AIMinelayer : AIUnit
{
    PathFinding _pathFinding;
    
    public override GameObject GetTarget()
    {
        _agent.destination = _pathFinding.GetRandomPosition();
        _currentTarget = null;
        return null;
    }

    public override void Awake()
    {
        base.Awake();
        _pathFinding = GetComponent<PathFinding>();
    }

    private void Update()
    {
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
            bomb.GetComponent<Bomb>().TimeToExplode = _bombTimeToExplode;
            bomb.GetComponent<Bomb>().ExplosionRange = _bombRange;
            bomb.GetComponent<Bomb>().ExplosionDamage = BombDamage;
            bomb.GetComponent<Bomb>().CurrentTeam = CurrentTeam;
            StartCoroutine(Reloading());
        }
    }
}
