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
        GetComponentInParents(gameObject).UnitDeathEvent.AddListener(StartSwitchRagdoll);
    }


    private void StartSwitchRagdoll()
    {
        transform.position = transform.position + Vector3.up * 1;
        SwitchRagdoll(gameObject);
        print("start switch ragdoll");
    }

    private void SwitchRagdoll(GameObject parent)
    {
        foreach(Transform child in parent.transform)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                print("rb is enabled / disabled on : " + child.name);
                rb.isKinematic = !rb.isKinematic;
                if (!rb.isKinematic)
                {
                    rb.AddForce((Camera.main.transform.position - transform.position).normalized * (25 + Random.Range(-2, 2)), ForceMode.VelocityChange);
                }
            }
            SwitchRagdoll(child.gameObject);
        }
    }
}
