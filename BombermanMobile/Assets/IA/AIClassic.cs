using UnityEngine;

public class AIClassic : AIUnit
{
    public override Vector3 GetTarget()
    {
        GameObject target = UnitManager.Instance.GetClosest(gameObject, CurrentTeam);

        if (target != null)
        {
            _agent.destination = target.transform.position;
        }
        return target.transform.position;
    }
}
