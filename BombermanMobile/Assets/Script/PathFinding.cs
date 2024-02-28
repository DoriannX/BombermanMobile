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
                _agent.destination = UnitManager.Instance.GetClosest(gameObject, _unitTeam).transform.position;
                break;
            case Type.Mortar:
                _agent.destination = UnitManager.Instance.GetFarthest(gameObject, _unitTeam).transform.position;
                break;
            case Type.Classic:
                _agent.destination = UnitManager.Instance.GetClosest(gameObject, _unitTeam).transform.position; //Player spawned but enemy is not
                break;
            case Type.Bowman:
                _agent.destination = UnitManager.Instance.GetClosest(gameObject, _unitTeam).transform.position;
                _agent.stoppingDistance = 10f;
                break;
            case Type.Minelayer:
                UnitManager.Instance.GetRandomPosition(_agent);
                break;
            case Type.Funky:
                _agent.destination = UnitManager.Instance.GetClosest(gameObject, _unitTeam).transform.position;
                break;
        }
        return target;
    }

    public void MoveTo()
    {
        GetTarget();
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
        }
        else
        {
            Debug.LogError("Camera is empty");
        }
    }
}
