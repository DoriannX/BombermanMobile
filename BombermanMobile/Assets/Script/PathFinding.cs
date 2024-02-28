using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class PathFinding : Unit
{
    private Vector3 _touchPos = Vector3.zero;
    private NavMeshAgent _agent = null;
    public Type _unitType = Type.Classic;
    public Team _unitTeam = Team.Player;
    private bool _canMove = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private Vector3 GetTarget()
    {
        Vector3 target = Vector3.zero;
        switch (_unitType)
        {
            case Type.Suicidal:
                target = UnitManager.Instance.GetClosest(gameObject, _unitTeam).transform.position;
                break;
            case Type.Mortar:
                target = UnitManager.Instance.GetFarthest(gameObject, _unitTeam).transform.position;
                break;
            case Type.Classic:
                target = UnitManager.Instance.GetClosest(gameObject, _unitTeam).transform.position; //Player spawned but enemy is not
                break;
            case Type.Bowman:
                target = UnitManager.Instance.GetClosest(gameObject, _unitTeam).transform.position;
                break;
            case Type.Minelayer:
                target = UnitManager.Instance.GetRandomPosition();
                break;
            case Type.Funky:
                target = UnitManager.Instance.GetClosest(gameObject, _unitTeam).transform.position;
                break;
        }
        return target;
    }

    public void MoveTo()
    {
        _agent.destination = GetTarget();
        _canMove = true;
    }

    private void FixedUpdate()
    {
        if(_canMove)
            MoveTo();
    }

    public void GetPosition(InputAction.CallbackContext ctx)
    {
        if(Camera.main != null)
        {
            _touchPos = ctx.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(_touchPos);
            if (Physics.Raycast(ray, out RaycastHit hitData) && hitData.collider.gameObject.CompareTag("Ground"))
            {
                _touchPos = hitData.point;
            }
            print(_touchPos);
        }
        else
        {
            Debug.LogError("Camera is empty");
        }
    }
}
