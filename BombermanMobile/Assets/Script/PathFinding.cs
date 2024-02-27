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
    private Vector3 GetTarget()
    {
        Vector3 target = Vector3.zero;
        switch (_characterType)
        {
            case Type.Suicidal:
                target = UnitManager.Instance.GetClosest(gameObject).transform.position;
                break;
            case Type.Mortar:
                target = UnitManager.Instance.GetFarthest(gameObject).transform.position;
                break;
            case Type.Classic:
                target = UnitManager.Instance.GetClosest(gameObject).transform.position;
                break;
            case Type.Bowman:
                target = UnitManager.Instance.GetClosest(gameObject).transform.position;
                break;
            case Type.Minelayer:
                target = GetRandomPosition();
                break;
            case Type.Funky:
                target = UnitManager.Instance.GetClosest(gameObject).transform.position;
                break;
        }
        return target;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPos = new Vector3(Random.Range(MapManager.Instance.MapGround.localScale.x*10/2, MapManager.Instance.MapGround.localScale.y * 10 / 2), 1);
        return randomPos;
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
