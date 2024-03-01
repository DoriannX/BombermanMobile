using UnityEngine;

public class Bomb : Unit
{
    public Team CurrentTeam; 
    public float TimeToExplode = 1.5f;
    public float ExplosionDamage;
    public float ExplosionRange;

    private bool _exploding = true;

    [SerializeField] private GameObject _particles;

    private void Start()
    {
        Destroy(gameObject, TimeToExplode);
    }

    private void OnDestroy()
    {
        Destroy(Instantiate(_particles, transform.position, Quaternion.identity), 5);
        if (_exploding)
        {
            foreach(Collider collider in Physics.OverlapSphere(transform.position, ExplosionRange)) 
            {
                if (collider.TryGetComponent<AIUnit>(out AIUnit unit))
                {
                    if (unit.CurrentTeam != CurrentTeam)
                    {
                        unit.TakeDamage(ExplosionDamage);
                    }
                }
                if (collider.CompareTag("Walls"))
                {
                    Destroy(collider);
                }
            }
        }
    }
}
