using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGoatScript : MonoBehaviour
{
    private float _baseHeight;
    [SerializeField] float _floatHeight = 0.5f;
    [SerializeField] float _floatSpeed = 1.5f;

    [SerializeField] float _rotateSpeed = 2;

    private void Awake()
    {
        _baseHeight = transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, _baseHeight + Mathf.Sin(Time.time * _floatSpeed) * _floatHeight, transform.position.z);
        transform.Rotate(new Vector3(0, _rotateSpeed * Time.deltaTime, 0));
    }
}
