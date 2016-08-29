using UnityEngine;
using System.Collections;

public class FireChargeStageThree : MonoBehaviour, ISpellCharging
{
    public ParticleSystem coreEffect;
    public ParticleSystem blaze;
    //public ParticleSystem sparks;
    public ParticleSystem trail;

    public void StartCharge()
    {
        coreEffect.Play();
        blaze.Play();
        //sparks.Play();
        trail.Play();
    }

    public void ChargeSpell(float chargeProgress)
    {

    }

    public void StopCharge()
    {
        coreEffect.Stop();
        blaze.Stop();
        //sparks.Stop();
        trail.Stop();
    }
}
