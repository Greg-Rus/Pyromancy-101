using UnityEngine;
using System.Collections;

public class FireChargeStageZero : MonoBehaviour, ISpellCharging
{
    public ParticleSystem coreEffect;
    public ParticleSystem sparks;

    public float startChargeScale = 0.2f;
    public float maxChargeScale = 1f;

    public void StartCharge()
    {
        coreEffect.Play();
        sparks.Play();
    }

    public void ChargeSpell(float chargeProgress)
    {
        float newScale = Mathf.Lerp(startChargeScale, maxChargeScale, chargeProgress);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    public void StopCharge()
    {
        coreEffect.Stop();
        sparks.Stop();
    }
}
