using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class PathFinding : Unit
{
    private Vector3 _touchPos = Vector3.zero;
    private NavMeshAgent _agent = null;
    AIUnit _ai;
    Vector3 randomPos = Vector3.zero;

    private bool _canGetRandomPos = true;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _ai = GetComponent<AIUnit>();
        if (_ai)
        {
            StartCoroutine(UpdatePath());
        }
        else
        {
            Debug.LogWarning("ai or target is empty");
        }
    }
    

    public void MoveTo()
    {
        _ai.GetTarget();
    }

    private void Update()
    {
    }

    private IEnumerator UpdatePath()
    {
        if (!_ai.IsDead)
        {
            _ai.GetTarget();
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UpdatePath());
    }

    public bool IsCloseToEnnemy(float range, GameObject target)
    {
        bool isCloseToEnnemy = false;

        if(Vector3.Distance(_agent.transform.position, target.transform.position) <= range && target!= null)
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
        if (Physics.Raycast(transform.position, transform.forward*10000, out RaycastHit hitInfo, range))
        {
            if (hitInfo.collider.CompareTag("Walls"))
            {
                return true;
            }
        }
        return false;
    }

    public GameObject GetClosestWall(GameObject unit, Team unitTeam)
    {
        List<GameObject> walls = new List<GameObject>();
        GameObject closest = null;

        foreach(Transform child in MapManager.Instance.gameObject.transform)
        {
            if (child.gameObject.CompareTag("Walls"))
            {
                walls.Add(child.gameObject);
            }
        }

        if (walls.Count > 0)
        {
            closest = walls[0];
            foreach (GameObject wall in walls)
            {
                if (Vector3.Distance(unit.transform.position, wall.transform.position) < Vector3.Distance(unit.transform.position, closest.transform.position))
                {
                    closest = wall;
                }
            }
        }
        return closest;
    }

    public Vector3 GetRandomPosition()
    {
        StartCoroutine(RandPosCo());
        return randomPos;
    }

    private IEnumerator RandPosCo()
    {
        if (_canGetRandomPos)
        {
            _canGetRandomPos = false;
            randomPos = new Vector3(Random.Range(
                -MapManager.Instance.MapGround.localScale.x * 10 / 2, MapManager.Instance.MapGround.localScale.x * 10 / 2),
                1,
                Random.Range(-MapManager.Instance.MapGround.localScale.z * 10 / 2, MapManager.Instance.MapGround.localScale.z * 10 / 2));
            print("random pos");
        }

        yield return new WaitForSeconds(3);
        StopAllCoroutines();
        _canGetRandomPos = true;
    }
}
