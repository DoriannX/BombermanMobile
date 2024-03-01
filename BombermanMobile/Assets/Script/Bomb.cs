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

    public bool IsThrowed = false;
    public float BombSpeed = 3;
    private Vector3 _basePosition;
    private float _maxHeight = 4;
    public Vector3 TargetPosition;
    private float _baseHeight;
    [SerializeField] private AnimationCurve _heightCurve;

    private void Awake()
    {
        _baseHeight = transform.position.y;
    }

    private void Start()
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
            float currentDistance = Vector3.Distance(_basePosition, transform.position);
            float proportion = currentDistance / totalDistance;
            proportion = Mathf.Clamp01(proportion);

            transform.position = new Vector3(transform.position.x, _baseHeight + (_heightCurve.Evaluate(proportion) * _maxHeight), transform.position.z);
            transform.position += (TargetPosition - transform.position).normalized * BombSpeed * Time.deltaTime;
            yield return null;
        }
        Detonate();
        yield return null;
    }

    private void Detonate()
    {
        StartCoroutine(OnDestroyBomb());
        Destroy(gameObject, TimeToExplode + .1f);
        _imageRadius.transform.localScale = new Vector3(1f, 1f, 1f) * ExplosionRange;
    }

    private IEnumerator OnDestroyBomb()
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
