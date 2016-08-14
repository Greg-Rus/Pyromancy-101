using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpellSoundController))]
[RequireComponent(typeof(Rigidbody))]
public class ProjectileSpellController : MonoBehaviour {
    SpellSoundController myAudio;
    ISpellMovement myMovement;
    ParticleSystem[] particleSystems;
    public bool isExplosive = false;
    public float explosionRadius;
    public float explosionForce; 

    public GameObject hitEffect;
    
    // Use this for initialization
    void Awake ()
    {
        myAudio = GetComponent<SpellSoundController>();
        myMovement = GetComponent<ISpellMovement>() as ISpellMovement;
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame

    void OnCollisionEnter(Collision collision)
    {
        StopParticleSystems();
        GameObject instance = Instantiate(hitEffect, transform.position, Quaternion.LookRotation(collision.contacts[0].normal, transform.up)) as GameObject;
        StartCoroutine(DespawnAfterTime(0.1f));
        if (isExplosive)
        {
            Explode();
        }
    }

    public void Cast(Vector3 worldDirection)
    {
        myAudio.PlayRandomSound();
        myMovement.StartMovement(worldDirection);
        PlayParticleSystems();
    }

    void PlayParticleSystems()
    {
        foreach (ParticleSystem system in particleSystems)
        {
            system.Play();
        }
    }
    void StopParticleSystems()
    {
        foreach (ParticleSystem system in particleSystems)
        {
            system.Stop();
        }
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hitColliders)
        {
            Rigidbody rigidBody = hit.GetComponent<Rigidbody>();

            if (rigidBody != null)
            {
                rigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius, 3.0f);
            }
        }
    }

    IEnumerator DespawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
