using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bomb : Unit
{
    [HideInInspector] public Team CurrentTeam;
    [HideInInspector] public UnityEvent bombExplodedEvent;
    [HideInInspector] public UnityEvent bombReachedTarget; //work on throwable only

    [HideInInspector] public float TimeToExplode = 1.5f;
    [HideInInspector] public float ExplosionDamage;
    [HideInInspector] public float ExplosionRange;

    [HideInInspector] public string OwnerName;

    protected private bool _exploding = true;

    [SerializeField] protected private Image _imageRadius;


    [HideInInspector] public bool IsThrowed = false;
    [HideInInspector] public float BombSpeed = 3;
    protected private Vector3 _basePosition;
    [HideInInspector] public float MaxHeight = 4;
    [HideInInspector] public Vector3 TargetPosition;
    protected private float _baseHeight;
    [SerializeField] protected private AnimationCurve _heightCurve;

    virtual protected void Awake()
    {
        _baseHeight = transform.position.y;
    }

    virtual protected void Start()
    {
        if (IsThrowed)
        {
            _baseHeight = transform.position.y;
            _basePosition = transform.position;
            StartCoroutine(LaunchBomb());
        }
        else
        {
            Detonate();
        }
    }

    private IEnumerator LaunchBomb()
    {
        while (Vector3.Distance(new Vector3(transform.position.x, TargetPosition.y, transform.position.z), TargetPosition) > .5f)
        {
            float totalDistance = Vector3.Distance(_basePosition, TargetPosition);
            float currentDistance = Vector3.Distance(_basePosition, new Vector3(transform.position.x, TargetPosition.y, transform.position.z));
            float proportion = currentDistance / totalDistance;

            transform.position = new Vector3(transform.position.x, _baseHeight + (_heightCurve.Evaluate(proportion) * MaxHeight), transform.position.z);
            transform.position += (TargetPosition - transform.position).normalized * BombSpeed * Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, TargetPosition.y, transform.position.z);
        Detonate();
        yield return null;
    }

    virtual protected void Detonate()
    {
        StartCoroutine(OnDestroyBomb());
        Destroy(gameObject, TimeToExplode + .1f);
        _imageRadius.transform.localScale = new Vector3(1f, 1f, 1f) * ExplosionRange;
    }

    virtual protected IEnumerator OnDestroyBomb()
    {
        yield return new WaitForSeconds(TimeToExplode);
        ParticleManager.Instance.ExplodeParticle(transform.position);
        if (_exploding)
        {
            foreach (Collider collider in Physics.OverlapSphere(transform.position, ExplosionRange/2))
            {
                if (collider.TryGetComponent<AIUnit>(out AIUnit unit))
                {
                    if (unit.CurrentTeam != CurrentTeam)
                    {
                        unit.LastDamageSourceName = OwnerName;
                        unit.TakeDamage(ExplosionDamage);
                    }
                }
                if (collider.CompareTag("Walls"))
                {
                    if(collider.gameObject.TryGetComponent<BonusBlock>(out BonusBlock component))
                    {
                        component.DestroyBlock();
                    }
                    Destroy(collider.gameObject);
                    ParticleManager.Instance.SpawnParticle(component.transform.position, ParticleManager.Instance.RockExplosionParticle);
                }
                if (collider.TryGetComponent<Mine>(out Mine mine))
                {
                    mine.Detonate();
                }
            }
            bombExplodedEvent.Invoke();
        }
    }
}
