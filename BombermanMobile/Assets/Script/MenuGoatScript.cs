using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGoatScript : MonoBehaviour
{
    private float _baseHeight;
    [SerializeField] float _floatHeight = 0.5f;
    [SerializeField] float _floatSpeed = 1.5f;

    [SerializeField] float _rotateSpeed = 2;

    private bool _isTouched = false;
    private Vector2 _fingerRotateSpeed = Vector2.zero;

    private void Awake()
    {
        _baseHeight = transform.position.y;
    }

    private void Start()
    {
        InputManager.Instance.TouchPositionEvent.AddListener(TouchRotate);
        InputManager.Instance.ReleaseClickEvent.AddListener(StopTouchRotate);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, _baseHeight + Mathf.Sin(Time.time * _floatSpeed) * _floatHeight, transform.position.z);
        transform.Rotate(new Vector3(0, _rotateSpeed * Time.deltaTime, 0));
        transform.Rotate(new Vector3(_fingerRotateSpeed.y * Time.deltaTime, _fingerRotateSpeed.x * Time.deltaTime, 0 * Time.deltaTime));
        _fingerRotateSpeed = Vector3.Lerp(_fingerRotateSpeed, Vector3.zero, 0.5f * Time.deltaTime);
    }


    private void TouchRotate()
    {
        InputManager input = InputManager.Instance;
        if (!_isTouched)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(input.LastTouchPosition.x, input.LastTouchPosition.y, 0));
            if (Physics.Raycast(ray, out RaycastHit hitData))
            {
                if (hitData.collider.gameObject == this.gameObject)
                {
                    _isTouched = true;
                }
                
            }
        }
        else
        {
            _fingerRotateSpeed += input.OldTouchPosition - input.LastTouchPosition;
            print(_fingerRotateSpeed);
        }
    }
        

    private void StopTouchRotate()
    {
        if (_isTouched)
        {
            _isTouched = false;
        }
    }
}
