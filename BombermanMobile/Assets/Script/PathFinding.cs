using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class PathFinding : Unit
{
    private Vector3 _touchPos = Vector3.zero;
    private NavMeshAgent _agent = null;
    [SerializeField] AIUnit _ai;
    private bool _canMove = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    

    public void MoveTo()
    {
        _ai.GetTarget();
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
