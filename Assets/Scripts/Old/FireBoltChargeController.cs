using UnityEngine;
using System.Collections;

public class FireBoltChargeController : MonoBehaviour {
    public GameObject flameOrb;
    public GameObject flameCharge;
    private ParticleSystem flameOrbParticleSystem;
    private ParticleSystem flameChargeParticleSystem;
    public float startChargeScale = 0.2f;
    public float maxChargeScale = 1f;
    // Use this for initialization
    void Awake()
    {
        flameOrbParticleSystem = flameOrb.GetComponent<ParticleSystem>();
        flameChargeParticleSystem = flameCharge.GetComponent<ParticleSystem>();
    }

    public void ScaleChargeEffect(float chargeScale)
    {
        float newScale = Mathf.Lerp(startChargeScale, maxChargeScale, chargeScale);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    public void StartCharge()
    {
        flameOrbParticleSystem.Play();
        flameChargeParticleSystem.Play();
    }

    public void StopCharge()
    {
        flameOrbParticleSystem.Stop();
        flameChargeParticleSystem.Stop();
    }

}
