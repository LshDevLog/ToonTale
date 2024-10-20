using UnityEngine;

public class PlayerBuffParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _plankPowerCharge, _plankFire;

    private void Update()
    {
        if (TempDataManager.Instance == null)
        {
            return;
        }

        ActivationParticle(_plankPowerCharge, TempDataManager.Instance._plankBuff);
        ActivationParticle(_plankFire, TempDataManager.Instance._plankFire);
    }

    private void ActivationParticle(ParticleSystem particle, bool input)
    {
        if (input && !particle.isPlaying)
        {
            particle.Play();
        }
        else if (input == false)
        {
            particle.Stop();
        }
    }
}
