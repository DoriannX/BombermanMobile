using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    
    private AIUnit GetComponentInParents(GameObject child)
    {
        AIUnit unit = child.GetComponentInParent<AIUnit>();
        if (unit == null)
        {
            return GetComponentInParents(child.transform.parent.gameObject);
        }
        else
        {
            return unit;
        }
    }

    private void Start()
    {
        GetComponentInParents(gameObject).UnitDeathEvent.AddListener(SwitchRagdoll);
    }
    private void SwitchRagdoll()
    {

    }
}
