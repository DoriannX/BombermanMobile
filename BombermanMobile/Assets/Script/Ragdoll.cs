using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    
    private AIUnit GetComponentInParents(GameObject child)
    {
        AIUnit unit = child.GetComponentInParent<AIUnit>();
        if (unit == null)
        {
            if (child.transform.parent != null)
            {
                print("recursif");
                return GetComponentInParents(child.transform.parent.gameObject);
            }
            else
            {
                Debug.LogError("GetComponent failed");
                return null;
            }
        }
        else
        {
            return unit;
        }
    }


    private void Update()
    {
        print("test");
    }

    private void Start()
    {
        SwitchRagdoll(gameObject);
        GetComponentInParents(gameObject).UnitDeathEvent.AddListener(delegate { SwitchRagdoll(gameObject); });
    }
    private void SwitchRagdoll(GameObject parent)
    {
        foreach(Transform child in parent.transform)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                print("rb is enabled / disabled");
                rb.isKinematic = !rb.isKinematic;
            }
            SwitchRagdoll(child.gameObject);
        }
    }
}
