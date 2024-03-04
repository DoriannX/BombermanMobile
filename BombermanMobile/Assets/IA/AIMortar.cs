using UnityEngine;

public class AIMortar : AIUnit
{
    PathFinding _pathFinding;

    public override GameObject GetTarget()
    {
        GameObject target = UnitManager.Instance.GetClosest(gameObject, CurrentTeam);

        if (target != null)
        {
            _currentTarget = target;
        }
        return target;
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
                ThrowBomb();
            }
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
            GameObject bomb = Instantiate(UnitManager.Instance.BombObject, gameObject.transform.position
                + (_bombPlacementOffset.z * transform.forward)
                + (_bombPlacementOffset.x * transform.right), Quaternion.identity);
            bomb.GetComponent<Bomb>().TimeToExplode = _bombTimeToExplode;
            bomb.GetComponent<Bomb>().ExplosionRange = _bombRange;
            bomb.GetComponent<Bomb>().ExplosionDamage = _bombDamage;
            bomb.GetComponent<Bomb>().CurrentTeam = CurrentTeam;
            StartCoroutine(Reloading());
        }
    }

    public void ThrowBomb()
    {
        if (CanAttack())
        {
            GameObject bomb = Instantiate(UnitManager.Instance.BombObject, gameObject.transform.position
                + (_bombPlacementOffset.z * transform.forward)
                + (_bombPlacementOffset.x * transform.right), Quaternion.identity);
            bomb.GetComponent<Bomb>().TargetPosition = _currentTarget.transform.position;
            bomb.GetComponent<Bomb>().TimeToExplode = _bombTimeToExplode;
            bomb.GetComponent<Bomb>().ExplosionRange = _bombRange;
            bomb.GetComponent<Bomb>().ExplosionDamage = _bombDamage;
            bomb.GetComponent<Bomb>().CurrentTeam = CurrentTeam;
            bomb.GetComponent<Bomb>().BombSpeed = 15;
            bomb.GetComponent<Bomb>().MaxHeight = 10;
            bomb.GetComponent<Bomb>().IsThrowed = true;
            StartCoroutine(Reloading());
        }
    }
}
