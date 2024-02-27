using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PathFinding : MonoBehaviour
{
    private enum Team {Player, Ennemy};
    private enum Type { Suicidal, Mortar, Classic, Bowman, Minelayer, Funky};
    [SerializeField] Team _characterTeam = Team.Player;
    [SerializeField] Type _characterType = Type.Classic; 
    List<GameObject> _enemiesUnit = new List<GameObject>();
    private Vector3 _touchPos = Vector3.zero;
    private NavMeshAgent _agent = null;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemiesUnit = UnitManager.Instance.EnemiesUnits;
    }

    private GameObject GetTarget()
    {
        GameObject target = null;
        switch (_characterType)
        {
            case Type.Suicidal:
                target = UnitManager.Instance.GetClosest(gameObject);
                break;
            case Type.Mortar:
                target = UnitManager.Instance.GetFarthest(gameObject);
                break;
            case Type.Classic:
                target = UnitManager.Instance.GetClosest(gameObject);
                break;
            case Type.Bowman:
                target = UnitManager.Instance.GetClosest(gameObject);
                break;
            case Type.Minelayer:
                //target = 
                break;
            case Type.Funky:
                break;
        }
        return target;
    }

    private Vector3 GetRandomPosition()
    {
        //Vector3 randomPos = new Vector3(Random.Range(-10, 10), Random.Range();
        return Vector3.zero;
    }

    public void MoveTo(InputAction.CallbackContext ctx)
    {
        _agent.destination = _touchPos;
    }

    public void GetPosition(InputAction.CallbackContext ctx)
    {
        _touchPos = ctx.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(_touchPos);
        if(Physics.Raycast(ray, out RaycastHit hitData)) 
        {
            _touchPos = hitData.point;
        }
        print(_touchPos);
    }
}
