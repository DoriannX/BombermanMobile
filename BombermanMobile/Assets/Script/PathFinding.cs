using System.Collections;
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
        StartCoroutine(UpdatePath());
    }
    

    public void MoveTo()
    {
        _ai.GetTargetPosition();
        _canMove = true;
    }

    private void FixedUpdate()
    {
        /*if(_canMove)
            MoveTo();*/
    }

    private IEnumerator UpdatePath()
    {
        _ai.GetTargetPosition();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UpdatePath());
    }

    public bool IsCloseToEnnemy(float range, GameObject target)
    {
        bool isCloseToEnnemy = false;

        if(target != null && Vector3.Distance(_agent.transform.position, target.transform.position) <= range)
        {
            isCloseToEnnemy = true;
        }

        return isCloseToEnnemy;
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

    public bool HasWallInFront(float range = 1.5f)
    {
        if (Physics.Raycast(transform.position, _agent.velocity.normalized, out RaycastHit hitInfo, range))
        {
            if (hitInfo.collider.CompareTag("Walls"))
            {
                return true;
            }
        }
        return false;
    }
}
