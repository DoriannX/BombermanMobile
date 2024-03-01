using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bomb : Unit
{
    public Team CurrentTeam;
    public UnityEvent bombExplodedEvent;
    public UnityEvent bombReachedTarget; //work on throwable only

    public float TimeToExplode = 1.5f;
    public float ExplosionDamage;
    public float ExplosionRange;

    private bool _exploding = true;

    [SerializeField] private Image _imageRadius;
    
    private void Start()
    {
        Detonate();
    }

    private void Detonate()
    {
        StartCoroutine(OnDestroyBomb());
        Destroy(gameObject, TimeToExplode + .1f);
        _imageRadius.transform.localScale = new Vector3(1f, 1f, 1f) * ExplosionRange;
    }

    IEnumerator OnDestroyBomb()
    {
        yield return new WaitForSeconds(TimeToExplode);
        ParticleManager.Instance.ExplodeParticle(transform.position);
        if (_exploding)
        {
            foreach (Collider collider in Physics.OverlapSphere(transform.position, ExplosionRange))
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
                    Destroy(collider.gameObject);
                }
            }
            bombExplodedEvent.Invoke();
        }
    }
}
