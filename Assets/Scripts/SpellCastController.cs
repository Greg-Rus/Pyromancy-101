using UnityEngine;
using System.Collections;

public class SpellCastController : MonoBehaviour {

    private GameObject spellInstance;

    public GameObject fireBolt;
    public GameObject fireMissile;
    public float projectileVelocityMultiplyer = 3f;

    public void CastSpell(SpellCharge spellChargeLevel, Vector3 velocity)
    {
        switch (spellChargeLevel)
        {
            case SpellCharge.FireOne: CastFireBolt(velocity); break;
            case SpellCharge.FireTwo: CastFireMissile(velocity); break;
        }
    }

    public void CastFireBolt(Vector3 worldVelocity)
    {
        spellInstance = Instantiate(fireBolt, transform.position, Quaternion.identity) as GameObject;
        spellInstance.GetComponent<ProjectileSpellHit>().Cast(worldVelocity * projectileVelocityMultiplyer);
        spellInstance = null;
    }

    public void CastFireMissile(Vector3 worldVelocity)
    {
        spellInstance = Instantiate(fireMissile, transform.position, Quaternion.identity) as GameObject;
        spellInstance.GetComponent<ProjectileSpellController>().Cast(worldVelocity);
        spellInstance = null;
    }
}
