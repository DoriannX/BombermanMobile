using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionParticle;
    public static ParticleManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }


    public void ExplodeParticle(Vector3 position)
    {
        Destroy(Instantiate(ExplosionParticle, position, Quaternion.identity), 5);
    }
}
