using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerScript : MonoBehaviour
{
    [SerializeField] private float _flickerFrequency = 1;
    private MeshRenderer _meshRenderer;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        ShowMesh();
    }

    private void ShowMesh()
    {
        _meshRenderer.enabled = true;
        Invoke("HideMesh", _flickerFrequency);
    }

    private void HideMesh()
    {
        _meshRenderer.enabled = false;
        Invoke("ShowMesh", _flickerFrequency);
    }
}
