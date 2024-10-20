using UnityEngine;

public class NormalAttackParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particle;
    
    private void OnEnable()
    {
        if(_particle != null)
        {
            _particle.Play();
        }

        Invoke("ReturnToPool", 1f);
    }


    private void ReturnToPool()
    {
        if(PoolManager.Instance == null)
        {
            return;
        }

        PoolManager.Instance._normalAttackPool.ReturnObj(this);
    }
}
