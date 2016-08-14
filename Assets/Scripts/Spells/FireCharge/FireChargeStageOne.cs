using UnityEngine;
using System.Collections;

public class FireChargeStageOne : MonoBehaviour, ISpellCharging
{
    public ParticleSystem coreEffect;
    public ParticleSystem sparks;

    public void StartCharge()
    {
        coreEffect.Play();
        sparks.Play();
    }

    public void ChargeSpell(float chargeProgress)
    {

    }

    public void StopCharge()
    {
        coreEffect.Stop();
        sparks.Stop();
    }
}
