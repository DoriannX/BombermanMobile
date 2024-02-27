using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PathFinding : MonoBehaviour
{
    
    private Vector3 _touchPos = Vector3.zero;
    private NavMeshAgent _agent = null;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
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
