using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] public GameObject ExplosionParticle;
    [SerializeField] public GameObject RockExplosionParticle;
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

    public void SpawnParticle(Vector3 position, GameObject SpawnPart)
    {
        Destroy(Instantiate(SpawnPart, position, Quaternion.identity), 5);
    }
}
