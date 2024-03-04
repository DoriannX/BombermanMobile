using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISuicidal : AIUnit
{
    PathFinding _pathFinding;
    private bool _exploding = false;
    public override GameObject GetTarget()
    {
        GameObject target = UnitManager.Instance.GetClosest(gameObject, CurrentTeam);

        if (_exploding)
        {
            _agent.destination = transform.position;
        }
        else
        {
            if (target != null)
            {
                _agent.destination = target.transform.position;
                _currentTarget = target;
            }
            else
            {
                _agent.destination = transform.position;
            }
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
        if (!_exploding)
        {
            if (_currentTarget)
            {
                if (_pathFinding.IsCloseToEnnemy(_range, _currentTarget) || _pathFinding.HasWallInFront(1.2f))
                {
                    Attack();
                }
            }
        }
        else
        {

        }
        
    }

    public override void Attack()
    {
        if (CanAttack() && !_exploding)
        {
            print("SUICIIIIIDE");
            _exploding = true;
            _agent.destination = transform.position;

            GameObject bomb = Instantiate(UnitManager.Instance.BombObject, gameObject.transform.position
                + (_bombPlacementOffset.z * transform.forward)
                + (_bombPlacementOffset.x * transform.right), Quaternion.identity, transform);
            bomb.GetComponent<Bomb>().TimeToExplode = _bombTimeToExplode;
            bomb.GetComponent<Bomb>().ExplosionRange = _bombRange;
            bomb.GetComponent<Bomb>().ExplosionDamage = BombDamage;
            bomb.GetComponent<Bomb>().CurrentTeam = CurrentTeam;
            
            bomb.GetComponent<Bomb>().bombExplodedEvent.AddListener(Suicide);
            StartCoroutine(Reloading());
        }
    }

    private void Suicide()
    {
        Death();
    }
}
