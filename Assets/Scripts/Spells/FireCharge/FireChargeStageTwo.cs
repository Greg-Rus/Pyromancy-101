using UnityEngine;
using System.Collections;

public class FireChargeStageTwo : MonoBehaviour, ISpellCharging
{

    public ParticleSystem coreEffect;
    public ParticleSystem blaze;
    public ParticleSystem sparks;
    public ParticleSystem trail;

    public void StartCharge()
    {
        coreEffect.Play();
        sparks.Play();
        blaze.Play();
        trail.Play();
    }

    public void ChargeSpell(float chargeProgress)
    {

    }

    public void StopCharge()
    {
        coreEffect.Stop();
        sparks.Stop();
        blaze.Stop();
        trail.Stop();
    }
}
