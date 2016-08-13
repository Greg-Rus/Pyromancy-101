using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpellSoundController))]
[RequireComponent(typeof(Rigidbody))]
public class ProjectileSpellController : MonoBehaviour {
    SpellSoundController myAudio;
    ISpellMovement myMovement;
    ParticleSystem[] particleSystems;

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

    IEnumerator DespawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
