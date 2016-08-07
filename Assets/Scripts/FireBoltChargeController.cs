using UnityEngine;
using System.Collections;

public class FireBoltChargeController : MonoBehaviour {
    public GameObject flameOrb;
    private ParticleSystem flameOrbParticleSystem;
    public float startChargeScale = 0.2f;
    public float maxChargeScale = 1f;
    // Use this for initialization
    void Awake()
    {
        flameOrbParticleSystem = flameOrb.GetComponent<ParticleSystem>();
    }

    public void ScaleChargeEffect(float chargeScale)
    {
        float newScale = Mathf.Lerp(startChargeScale, maxChargeScale, chargeScale);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

}
