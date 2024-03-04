using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoatPortrait : Unit, IPointerDownHandler
{
    private bool _isClicked = false;
    private bool _alreadyFollowing = false;

    private Transform _unitVisual; //unit that is following finger

    [SerializeField] private Team _team;
    [SerializeField] private Type _type;
    [SerializeField] private Sprite _spriteRadius;

    private Vector2 _placeOffset = Vector2.zero;

    

    public void FollowFinger()
    {
        Vector2 fingerPos = InputManager.Instance.LastTouchPosition + _placeOffset;
        if (_isClicked)
        {
            if (!_alreadyFollowing)
            {
                _alreadyFollowing = true;
                GameObject newObject = new GameObject();
                newObject.AddComponent<Image>();
                newObject.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
                _unitVisual = Instantiate(newObject, fingerPos, Quaternion.identity, transform.parent.parent).transform;
                Instantiate(newObject, _unitVisual).GetComponent<Image>().sprite = _spriteRadius;
                _unitVisual.GetChild(0).transform.localScale = new Vector3(1, 1, 1) * 5f;
                Destroy(newObject);
            }
            else
            {
                _unitVisual.position = new Vector3(fingerPos.x, fingerPos.y, 0);
            }
            _unitVisual.GetChild(0).GetComponent<Image>().color = (CanSpawnUnitAt(fingerPos)) ? Color.green : Color.red;
            _unitVisual.GetChild(0).GetComponent<Image>().color += new Color(0, 0, 0, -0.8f);
        }
    }

    private void UnfollowFinger()
    {
        if (_unitVisual)
        {
            SpawnUnit(InputManager.Instance.LastTouchPosition + _placeOffset);
            Destroy(_unitVisual.gameObject);
            _unitVisual = null;
        }
        InputManager.Instance.TouchPositionEvent.RemoveListener(FollowFinger);
        _alreadyFollowing = false;
        _isClicked = false;
    }

    public void SpawnUnit(Vector2 spawnPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(spawnPosition.x, spawnPosition.y, 0));
        GameObject unitToSpawn = UnitManager.Instance.GetUnitToSpawn(_type);
        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            if (CanSpawnUnitAt(spawnPosition))
            {
                if(unitToSpawn != null)
                {
                    UnitManager.Instance.SpawnUnit(_team, 1, hitData.point, unitToSpawn);
                }
                else
                {
                    Debug.LogError("You didn't put the unit to spawn in the serialize field");
                }
            }
        }
    }

    private bool CanSpawnUnitAt(Vector2 spawnPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(spawnPosition.x, spawnPosition.y, 0));
        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            if (hitData.collider.CompareTag("Ground"))
            {
                float sphereRadius = 0.8f;
                foreach (Collider collider in Physics.OverlapSphere(hitData.point, sphereRadius))
                {

                    if (!collider.CompareTag("Ground"))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        return false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClicked = true;
        FollowFinger();
        InputManager.Instance.TouchPositionEvent.AddListener(FollowFinger);
        InputManager.Instance.ReleaseClickEvent.AddListener(UnfollowFinger);
    }
}
