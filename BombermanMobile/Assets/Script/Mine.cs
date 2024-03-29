using Unity.VisualScripting;
using UnityEngine;

public class Mine : Bomb
{
    [SerializeField] float range = .5f;
    SphereCollider collider;
    protected override void Start()
    {
        collider = GetComponent<SphereCollider>();
        if (collider)
        {
            collider.radius = range;
        }
        else
        {
            Debug.LogWarning("collider is empty");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AIUnit>(out AIUnit unit))
        {
            if(unit.CurrentTeam != CurrentTeam)
            {
                print("detected");
                Detonate();
            }
        }
    }
}