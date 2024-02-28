using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoatPortrait : MonoBehaviour, IPointerDownHandler
{
    private bool _isClicked = false;
    private bool _alreadyFollowing = false;

    private Transform _unitVisual; //unit that is following finger

    [SerializeField] private GameObject _unitPrefab;
    
    private void Awake()
    {
        
    }

    public void FollowFinger()
    {
        Vector2 fingerPos = InputManager.Instance.LastTouchPosition;
        if (_isClicked)
        {
            if (!_alreadyFollowing)
            {
                _alreadyFollowing = true;
                GameObject newObject = new GameObject();
                newObject.AddComponent<Image>();
                newObject.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
                _unitVisual = Instantiate(newObject, InputManager.Instance.LastTouchPosition, Quaternion.identity, transform.parent.parent).transform;
                Destroy(newObject);
            }
            else
            {
                _unitVisual.position = new Vector3(fingerPos.x, fingerPos.y, 0);
            }
        }
    }

    private void UnfollowFinger()
    {
        if (_unitVisual)
        {
            SpawnUnit(InputManager.Instance.LastTouchPosition);
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
        print("tryspawn");
        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            print(hitData.collider.name);
            if (hitData.collider.CompareTag("Ground") || true)
            {
                Instantiate(_unitPrefab, hitData.point, Quaternion.identity);
                print("spawned");
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClicked = true;
        FollowFinger();
        InputManager.Instance.TouchPositionEvent.AddListener(FollowFinger);
        InputManager.Instance.ReleaseClickEvent.AddListener(UnfollowFinger);
    }
}
