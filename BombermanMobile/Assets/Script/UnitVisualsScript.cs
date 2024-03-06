using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVisualsScript : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer _penguinMesh;
    [SerializeField] SkinnedMeshRenderer _goatMesh;
    private Color _penguinColor;
    private Color _goatColor;

    private void Start()
    {
        if (transform.parent.TryGetComponent<AIUnit>(out AIUnit unit))
        {
            unit.TakeDamageEvent.AddListener(ShowFlashDamage);
        }
        _penguinColor = _penguinMesh.material.color;
        _goatColor = _goatMesh.material.color;
    }

    public void ShowFlashDamage()
    {
        _penguinMesh.material.color = Color.red;
        _goatMesh.material.color = Color.red;
        Invoke("HideFlashDamage", 0.45f);
    }

    private void HideFlashDamage()
    {
        _penguinMesh.material.color = _penguinColor;
        _goatMesh.material.color = _goatColor;
    }
}
