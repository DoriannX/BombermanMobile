using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public UnityEvent ClickEvent;
    public UnityEvent ReleaseClickEvent;
    public UnityEvent TouchPositionEvent;

    private Vector2 _oldTouchPosition; public Vector2 OldTouchPosition { get { return _oldTouchPosition; } }
    private Vector2 _lastTouchPosition; public Vector2 LastTouchPosition { get { return _lastTouchPosition; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    public void OnTouchPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ClickEvent.Invoke();
        }
        if (context.canceled)
        {
            ReleaseClickEvent.Invoke();
        }
    }

    public void OnTouchPosition(InputAction.CallbackContext context)
    {
        _oldTouchPosition = _lastTouchPosition;
        _lastTouchPosition = context.ReadValue<Vector2>();
        TouchPositionEvent.Invoke();
    }
}
